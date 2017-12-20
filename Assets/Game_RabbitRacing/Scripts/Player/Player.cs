using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public Image m_miniMapIcon;


	// Use this for initialization
	void Start () {
	    MinimapController.RegisterMapIcon(gameObject, m_miniMapIcon);
	}
	
	// Update is called once per frame
	void Update () {
	    if (!isLocalPlayer) {
	        return;
	    }
	}

    void OnDestroy() {
        MinimapController.RemoveMapIcons(gameObject);
    }
}
