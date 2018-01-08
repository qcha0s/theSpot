using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerNameInput(string name) {
		PlayerPrefs.SetString("PlayerName", name);
		Debug.Log("name set to " + name);
	}

}
