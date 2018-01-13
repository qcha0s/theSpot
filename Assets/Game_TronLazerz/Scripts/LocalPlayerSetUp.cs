using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayerSetUp : NetworkBehaviour {
	[SyncVar]
	public int team = 0;

	private bool colorSet = false;
	 SpriteRenderer m_sr;



	 

	// Use this for initialization
	public override void OnStartLocalPlayer(){
		GetComponent<TronPlayerMovement>().enabled=true;
		if(isServer){
			CmdTellServerTeam(1);
			
		}else{
			
			CmdTellServerTeam(2);
		}
	NetworkManager.singleton.GetComponent<NetworkManagerHUD>().showGUI = false;
	}

	[Command]

	public void CmdTellServerTeam(int teamInt){
		team = teamInt;
	}

	
	
	// Update is called once per frame
	void Update () {
		if(team != 0 && !colorSet){
			if(team == 1){
				SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
				renderer.color =  Color.red;
			}else{
				SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
				renderer.color =  Color.blue;
			}
			colorSet = true;
			
		}
	}
}
