using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayerSetUp : NetworkBehaviour {

	 SpriteRenderer m_sr;

	// Use this for initialization
	public override void OnStartLocalPlayer(){
		GetComponent<TronPlayerMovement>().enabled=true;
		if(isServer){
			SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
			renderer.color =  Color.blue;
		}else{
			SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
			renderer.color =  Color.red;
		}
		
	}

	
	
	// Update is called once per frame
	void Update () {
		
	}
}
