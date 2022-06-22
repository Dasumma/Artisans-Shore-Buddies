using TMPro;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Button startServerButton;
	[SerializeField]
	private Button startHostButton;
	[SerializeField]
	private Button startClientButton;
	[SerializeField]
	private TMP_InputField joinCodeInput;

	private bool hasServerStarted;
	
	private void Awake()
	{
		
	}
	
	void start()
	{
		startHostButton?.onClick.AddListener(async () =>
		{
			
			if (RelayManager.Instance.IsRelayEnabled)
				await RelayManager.Instance.SetupRelay();
			if (NetworkManager.Singleton.StartHost())
				Logger.Instance.LogInfo("Host started...");
			else
				Logger.Instance.LogInfo("Unable to start host...");
			SceneManager.LoadScene("Level 1");
		});
		
		startClientButton?.onClick.AddListener(async () =>
		{
			if (RelayManager.Instance.IsRelayEnabled && !string.IsNullOrEmpty(joinCodeInput.text))
				await RelayManager.Instance.JoinRelay(joinCodeInput.text);
			if (NetworkManager.Singleton.StartHost())
				Logger.Instance.LogInfo("Client started...");
			else
				Logger.Instance.LogInfo("Unable to start client...");
		});
	}
}
