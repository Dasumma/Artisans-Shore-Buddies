using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class collectableScript : MonoBehaviourPun
{
    public string itemType;
	public int itemWeight;
	public int itemValue;
	
	public void CallToDelete()
	{
		if (photonView.Owner == PhotonNetwork.LocalPlayer)
		{
			photonView.RPC(nameof(DestroyObject), RpcTarget.AllBuffered);
		}
		else
		{
			TransferOwnership();
		}
	}
	void TransferOwnership()
	{
		photonView.RequestOwnership();
		photonView.RPC(nameof(DestroyObject), RpcTarget.AllBuffered);
	}
	[PunRPC]
	public void DestroyObject()
	{
		Destroy(gameObject);
	}
}
