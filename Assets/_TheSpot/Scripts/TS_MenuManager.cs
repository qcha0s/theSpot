using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_MenuManager : MonoBehaviour {

	public void PlayerNameInput(string name) {
		PlayerPrefs.SetString("PlayerName", name);
		Debug.Log("name set to " + name);
	}

}
