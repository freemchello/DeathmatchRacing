using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerChoice : MonoBehaviour
{
    public Text playerName;

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
    }
}
