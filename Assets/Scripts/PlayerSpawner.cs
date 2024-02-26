using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    [Space]
    public Transform[] spawnPoints;

    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("playerCars"))
        {
            int playerIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["playerCars"];
            if (playerIndex >= 0 && playerIndex < playerPrefabs.Length)
            {
                GameObject playerToSpawn = playerPrefabs[playerIndex];
                GameObject _player = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
                _player.GetComponent<PlayerSetup>().IsLocalPlayer();
            }
            else
            {
                Debug.LogError("Invalid player car index: " + playerIndex);
            }
        }
        else
        {
            Debug.LogError("Player car property not found");
        }
    }
}
