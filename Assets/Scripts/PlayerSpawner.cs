using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("playerCars"))
        {
            int carIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["playerCars"];
            if (carIndex >= 0 && carIndex < playerPrefabs.Length)
            {
                GameObject playerToSpawn = playerPrefabs[carIndex];
                PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Invalid player car index: " + carIndex);
            }
        }
        else
        {
            Debug.LogError("Player car property not found");
        }
    }
}
