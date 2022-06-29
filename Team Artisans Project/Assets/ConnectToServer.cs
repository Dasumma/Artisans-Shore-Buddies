using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField usernameInput;
    public Text buttonText;


    public void OnClickConnect()
    {
        if(usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

	public Animator transition;
    // Start is called before the first frame update

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LoadLevel("Lobby");

    }
	
	IEnumerator LoadLevel()
	{
        transition.SetTrigger("Start");
		yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel("Lobby");
	}
	
    /*public override void OnJoinedLobby()
    {
        StartCoroutine(LoadLevel());
    }*/
}