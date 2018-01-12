using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TS_CustomNetworkManager : NetworkManager {

	public static TS_CustomNetworkManager Instance { get { return m_instance; }}
	public int m_netPort = 7777;

	private static TS_CustomNetworkManager m_instance = null;
	public bool isHost = false;
	public NetworkClient m_client;

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
		// if (m_client == null) {
			StartCoroutine(CheckForHost());
		// } else {
		// 	m_client.connection.Disconnect();
		// 	m_client = null;
		// 	StartCoroutine(CheckForHost());
		// }
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
