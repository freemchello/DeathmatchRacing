using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private GameObject roomUI;
    [SerializeField] private GameObject lobbyUI;

    [SerializeField] private RoomPrefab roomPrefab;
    [SerializeField] private Transform contentObject;

    private List<RoomPrefab> roomPrefabs = new List<RoomPrefab>();

    [SerializeField] private float updateInterval;
    private float nextUpdateTime;

    public List<PlayerChoice> playerChoiceList = new List<PlayerChoice>();
    public PlayerChoice PlayerChoicePrefab;
    public Transform playerChoiceParent;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        if (roomNameInputField.text != "") 
        {
            PhotonNetwork.CreateRoom(roomNameInputField.text, new RoomOptions() { MaxPlayers = 6 });
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyUI.SetActive(false);
        roomUI.SetActive(true);
        roomName.text = "Комната: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime) 
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + updateInterval;
        }
    }

    private void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomPrefab room in roomPrefabs)
        {
            Destroy(room.gameObject);
        }
        roomPrefabs.Clear();
        
        foreach (RoomInfo room in list)
        {
            RoomPrefab newRoom = Instantiate(roomPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomPrefabs.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        lobbyUI.SetActive(true);
        roomUI.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate()
    {
        if(roomNameInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomNameInputField.text, new RoomOptions() { MaxPlayers = 6, BroadcastPropsChangeToAll = true});
        }
    }

    void UpdatePlayerList()

    {
        foreach(PlayerChoice choice in playerChoiceList)
        {
            Destroy(choice.gameObject);
        }
        playerChoiceList.Clear();

        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerChoice newPlayerChoice = Instantiate(PlayerChoicePrefab, playerChoiceParent);
            newPlayerChoice.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerChoice.ApplyLocalChanges();
            }

            playerChoiceList.Add(newPlayerChoice);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }
}
