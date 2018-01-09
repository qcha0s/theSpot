using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : MonoBehaviour {

    public Image m_miniMapIcon;

    //public override void OnLocoalPlayer() {
    //    tag = "Player";
    //}
	
    
    // Use this for initialization
	void Start () {
	    MinimapController.RegisterMapIcon(gameObject, m_miniMapIcon);
	}
	
	// Update is called once per frame
	void Update () {
	    //if (!isLocalPlayer) {
	    //    return;
	    //}
	}

    void OnDestroy() {
        MinimapController.RemoveMapIcons(gameObject);
    }
}
