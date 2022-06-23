using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Sammy(clone)");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Sammy(Clone)");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
