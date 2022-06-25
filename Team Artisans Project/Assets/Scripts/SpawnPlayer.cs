using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> playerPrefab;
	public List<GameObject> trashItems;
	public int trashQuantity;
    public float minX, maxX, minY, maxY;
	public float borderMinX, borderMaxX, borderMinY, borderMaxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
		if(PhotonNetwork.IsMasterClient)
		{
			for(int i = 0; i < trashQuantity; i++)
			{
				randomPosition = new Vector2(Random.Range(borderMinX, borderMaxX), Random.Range(borderMinY, borderMaxY));
				PhotonNetwork.Instantiate(trashItems[Random.Range(0, 5)].name, randomPosition, Quaternion.identity);
			}
		}
    }

}
