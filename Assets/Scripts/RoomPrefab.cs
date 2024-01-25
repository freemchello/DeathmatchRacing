using TMPro;
using UnityEngine;

public class RoomPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    private LobbyManager manager;

    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string nameToDisplay)
    {
        roomName.text = nameToDisplay;
    }

    public void Join()
    {
        manager.JoinRoom(roomName.text);
    }
}
