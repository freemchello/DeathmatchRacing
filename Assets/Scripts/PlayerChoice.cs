using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerChoice : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerName;

    //Image blackgroundImage;
    public Color highlightColor;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerCars;
    public Sprite[] avatars;

    Player player;

    //private void Awake()
    //{
    //    blackgroundImage = GetComponent<Image>();
    //}
    private void Start()
    {
        playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;

        if (!playerProperties.ContainsKey("playerCars"))
        {
            playerProperties["playerCars"] = 0;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
            UpdatePlayerCarImage();
        }
    }

    private void UpdatePlayerCarImage()
    {
        if (playerProperties.ContainsKey("playerCar"))
        {
            int carIndex = (int)playerProperties["playerCar"];
            playerCars.sprite = avatars[carIndex];
        }
    }

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerChoice(player);
    }

    public void ApplyLocalChanges()
    {
        //blackgroundImage.color = highlightColor;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
    }

    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerCars"] == 0)
        {
            playerProperties["playerCars"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerCars"] = (int)playerProperties["playerCars"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerCars"] == avatars.Length - 1)
        {
            playerProperties["playerCars"] = 0;
        }
        else
        {
            playerProperties["playerCars"] = (int)playerProperties["playerCars"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerChoice(targetPlayer);
        }
    }

    void UpdatePlayerChoice(Player targetPlayer)
    {
        if (player.CustomProperties.ContainsKey("playerCars"))
        {
            playerCars.sprite = avatars[(int)player.CustomProperties["playerCars"]];
            playerProperties["playerCars"] = (int)player.CustomProperties["playerCars"];
        }
        else
        {
            playerProperties["playerCars"] = 0;
        }
    }
}
