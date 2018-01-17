using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
public class TronNetworkManager : NetworkManager {

	public ScoreManager m_sm;

	void OnPlayerConnected(NetworkPlayer player){
		Debug.Log("my name is DA WAE");
		
	}

}
