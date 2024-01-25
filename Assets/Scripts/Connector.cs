using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class Connector : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_Text buttonText;

    public void ConnectedToServer()
    {
        if(usernameInput.text != "")
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Подключение...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}
