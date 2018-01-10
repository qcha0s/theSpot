using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TS_InGameUIManager : NetworkBehaviour {

    public GameObject m_menu;
    public Button m_DisconnectButton;
    public EventSystem m_system;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            m_menu.SetActive(!m_menu.activeInHierarchy);
        }
        if (m_menu.activeInHierarchy ) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

	public void ExitGame() {
        TS_CustomNetworkManager.Instance.DisconnectFromGame();
    }
}
