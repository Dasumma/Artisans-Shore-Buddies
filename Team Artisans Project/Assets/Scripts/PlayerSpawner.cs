using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    public float minX, maxX, minY, maxY;
    public float borderMinX, borderMaxX, borderMinY, borderMaxY;
    public int trashQuantity;
	public int obstacleQuantity;
    public List<GameObject> trashItems;
	public List<GameObject> Obstacles;

    private void Start()
    {
        int randomNumber = Random.Range(0,spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
		print(PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]);
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name,spawnPoint.position,Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < trashQuantity; i++)
            {
                Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                PhotonNetwork.Instantiate(trashItems[Random.Range(0, trashItems.Count)].name, randomPosition, Quaternion.Euler(0,0,Random.Range(0,360)));
            }
            for (int i = 0; i < obstacleQuantity; i++)
            {
                Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                PhotonNetwork.Instantiate(Obstacles[Random.Range(0, Obstacles.Count)].name, randomPosition, Quaternion.identity);
            }
        }
    }
}
