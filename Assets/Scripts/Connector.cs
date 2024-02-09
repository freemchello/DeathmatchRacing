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
    [SerializeField] private TMP_InputField _usernameInputReg;
    [SerializeField] private TMP_InputField _passwordInputReg;
    [SerializeField] private TMP_InputField _emailInputReg;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;

    

    [SerializeField] private TMP_Text buttonText;

    private const string AuthGuidKey = "1593578264";

    public UnityEvent OnSuccessEvent;
    public UnityEvent OnFailureEvent;

    private bool isLogIn = false;

    //private void Start()
    //{
    //    var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
    //    var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());

    //    var request = new LoginWithCustomIDRequest
    //    {
    //        CustomId = id,
    //        CreateAccount = !needCreation
    //    };

    //    PlayFabClientAPI.LoginWithCustomID(request,
    //        result =>
    //        {
    //            Debug.Log(result.PlayFabId);
    //            PhotonNetwork.AuthValues = new AuthenticationValues(result.PlayFabId);
    //            PhotonNetwork.NickName = result.PlayFabId;
    //        },
    //        error => Debug.LogError(error));


    //}
    public void ConnectedToServer()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMasterPlayfab");
        OnSuccessEvent.Invoke();

        if (isLogIn == true)
        {

            PhotonNetwork.NickName = _usernameInput.text;
            buttonText.text = "Подключение...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = PhotonNetwork.AppVersion;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        OnFailureEvent.Invoke();
        Debug.Log("Отключен");
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

    public void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _usernameInputReg.text,
            //Email = _emailInputReg.text,
            Password = _passwordInputReg.text,
            RequireBothUsernameAndEmail = false
        }, result =>
        {
            Debug.Log($"Success:{_usernameInputReg}");
            isLogIn = true;
            ConnectedToServer();
        }, error =>
        {
            isLogIn = false;
            OnLoginError(error);
            Debug.LogError($"Error: {error.ErrorMessage}");
        });
    }

    public void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _usernameInput.text,
            Password = _passwordInput.text
        }, result =>
        {
            Debug.Log($"Success:{_usernameInput.text}");
            isLogIn = true;
            ConnectedToServer();
        }, error =>
        {
            isLogIn = false;
            Debug.LogError($"Error: {error.ErrorMessage}");
        });
    }
}
