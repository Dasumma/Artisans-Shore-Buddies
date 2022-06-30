using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject[] playerPrefabs;
	public List<GameObject> trashItems;
	public int trashQuantity;
	public int playerChoice;
    public float minX, maxX, minY, maxY;
	public float borderMinX, borderMaxX, borderMinY, borderMaxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
		GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatars"]];
		PhotonNetwork.Instantiate(playerToSpawn.name, randomPosition, Quaternion.identity);
		if (PhotonNetwork.IsMasterClient)
		{
			for(int i = 0; i < trashQuantity; i++)
			{
				randomPosition = new Vector2(Random.Range(borderMinX, borderMaxX), Random.Range(borderMinY, borderMaxY));
				PhotonNetwork.Instantiate(trashItems[Random.Range(0,6)].name, randomPosition, Quaternion.identity);
			}
		}
    }

}
