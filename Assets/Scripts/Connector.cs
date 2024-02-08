using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using PlayFab.ClientModels;
using PlayFab;
using Photon.Realtime;
using UnityEngine.Events;
using System;

public class Connector : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Text buttonText;

    private const string AuthGuidKey = "1593578264";

    public UnityEvent OnSuccessEvent;
    public UnityEvent OnFailureEvent;

    private bool isLogIn;

    private void Start()
    {
        var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
        var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());

        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = !needCreation
        };

        PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                Debug.Log(result.PlayFabId);
                PhotonNetwork.AuthValues = new AuthenticationValues(result.PlayFabId);
                PhotonNetwork.NickName = result.PlayFabId;
            },
            error => Debug.LogError(error));
    }
    public void ConnectedToServer()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMasterPlayfab");
        OnSuccessEvent.Invoke();

        if (usernameInput.text != "")
        {


            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "�����������...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = PhotonNetwork.AppVersion;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        OnFailureEvent.Invoke();
        Debug.Log("��������");
    }
    private void OnLoginError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError(errorMessage);
        OnFailureEvent.Invoke();
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}
