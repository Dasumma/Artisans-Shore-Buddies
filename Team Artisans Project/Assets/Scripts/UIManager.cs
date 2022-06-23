using TMPro;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private TMP_InputField joinCodeInput;

	private bool hasServerStarted;
	
	public void ButtonPress()
	{
		//SceneManager.LoadScene("Level 1");
		StartHostButton();
	}

	public async void StartHostButton()
	{
		if (RelayManager.Instance.IsRelayEnabled)
			await RelayManager.Instance.SetupRelay();

		if (NetworkManager.Singleton.StartHost())
			Logger.Instance.LogInfo("Host started...");

		else
			Logger.Instance.LogInfo("Unable to start host...");
		
		GameObject.Find("Sammy(Clone)").name = "Player";
	}
	public async void StartClientButton()
	{
		if (RelayManager.Instance.IsRelayEnabled && !string.IsNullOrEmpty(joinCodeInput.text))
			await RelayManager.Instance.JoinRelay(joinCodeInput.text);
		if (NetworkManager.Singleton.StartClient())
			Logger.Instance.LogInfo("Client started...");
		else
			Logger.Instance.LogInfo("Unable to start client...");
		GameObject.Find("Sammy(Clone)").name = "Client";
	}
}
