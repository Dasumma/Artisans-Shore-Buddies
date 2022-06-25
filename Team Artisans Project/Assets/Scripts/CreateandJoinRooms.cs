using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateandJoinRooms : MonoBehaviourPunCallbacks
{
	public Animator transition;
	public GameObject trashObject;
    public InputField createInput;
    public InputField joinInput;
   
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
		StartCoroutine(DoTransition());
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
		StartCoroutine(DoTransition());
        PhotonNetwork.AutomaticallySyncScene = true;
    }
	
	IEnumerator DoTransition()
	{
        transition.SetTrigger("Start");
		yield return new WaitForSeconds(1);
	}
	
	
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Level 1");
    }

}
