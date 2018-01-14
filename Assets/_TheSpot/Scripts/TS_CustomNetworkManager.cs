using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class TS_CustomNetworkManager : NetworkManager {

	public static TS_CustomNetworkManager Instance { get { return m_instance; }}
	public int m_netPort = 7777;

	private static TS_CustomNetworkManager m_instance = null;
	public bool isHost = false;
	public NetworkClient m_client;
	public GameObject m_localPlayer;

	private void Awake() {
		if (m_instance != null && m_instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			m_instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start() {
		SetPort();
	}

	public void HostOrJoin() {
		isHost = false;
		StartCoroutine(CheckForHost());
	}

	private void StartupHost() {
		m_client = StartHost();
	}

	void SetPort() {
		networkPort = m_netPort;
	}

	public void DisconnectFromGame() {
		if (isHost) {
			Debug.Log("Host disconnected");
			NetworkManager.singleton.StopHost();
		} else {
			Debug.Log("client disconnected");
			NetworkManager.singleton.StopClient();
		}
	}

	public void DisablePlayer() {
		TS_SoundManager.Instance.StopMusic();
//		m_localPlayer.GetComponentInChildren<Camera>().enabled = false;
		m_localPlayer.GetComponentInChildren<AudioListener>().enabled = false;
		m_localPlayer.GetComponent<FirstPersonController>().enabled = false;	
	}

	public void EnablePlayer() {
		TS_SoundManager.Instance.StartMusic();
//		m_localPlayer.GetComponentInChildren<Camera>().enabled = true;
		m_localPlayer.GetComponentInChildren<AudioListener>().enabled = true;
		m_localPlayer.GetComponent<FirstPersonController>().enabled = true;			
	}

	IEnumerator CheckForHost() {
		m_client = StartClient();
		yield return new WaitForSeconds(3);
		if (!m_client.connection.isReady) {
			m_client.Disconnect();
			isHost = true;
			StartupHost();
		}
	}
}
