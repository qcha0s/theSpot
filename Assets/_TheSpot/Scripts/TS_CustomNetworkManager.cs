using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TS_CustomNetworkManager : NetworkManager {

	public static TS_CustomNetworkManager Instance { get { return m_instance; }}
	public int m_netPort = 7777;

	private static TS_CustomNetworkManager m_instance = null;
	private bool isHost = false;

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
		StartCoroutine(CheckForHost());
	}

	private void StartupHost() {
		StartHost();
	}

	void SetPort() {
		networkPort = m_netPort;
	}

	// public string SetIpAdress() {
	// 	return 
	// }

	IEnumerator CheckForHost() {
		NetworkClient newClient = StartClient();
		yield return new WaitForSeconds(3);
		if (!newClient.connection.isReady) {
			Debug.Log("not connected");
			newClient.Disconnect();
			isHost = true;
			StartupHost();
		}		
	}
}
