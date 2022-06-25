using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
	public Animator transition;
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
	
	IEnumerator LoadLevel()
	{
        transition.SetTrigger("Start");
		yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel("Lobby");
	}
	
    public override void OnJoinedLobby()
    {
        StartCoroutine(LoadLevel());
    }
}