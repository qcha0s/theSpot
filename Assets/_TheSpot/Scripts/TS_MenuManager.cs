using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TS_MenuManager : MonoBehaviour {

	public Button m_joinGameButton;

	private void Start() {
		CheckNameValidity();
	}

	public void PlayerNameInput(string name) {
		PlayerPrefs.SetString("PlayerName", name);
		CheckNameValidity();
	}

	public void StartGame() {
		TS_CustomNetworkManager.Instance.HostOrJoin();
		m_joinGameButton.interactable = false;
	}

	public void QuitGame() {
		Application.Quit();
	}

	private void CheckNameValidity() {
		if (PlayerPrefs.GetString("PlayerName") != null && PlayerPrefs.GetString("PlayerName") != "") {
			m_joinGameButton.interactable = true;
		} else {
			m_joinGameButton.interactable = false;
		}
	}
}
