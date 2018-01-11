using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnableNetworkScripts : NetworkBehaviour {

	public int m_chosenChar = 0;
	public GameObject m_mage;
	public GameObject m_warrior;
	public GameObject m_rogue;
	public GameObject m_hunter;

	void Start() {
		if(isLocalPlayer) {
			GetComponent<RPGCharacterController>().enabled = true;
			
			switch(m_chosenChar) {
				case 0:
					m_mage.GetComponent<BS_Mage>().enabled = true;
					break;
				case 1:
					m_warrior.GetComponent<BS_Warrior>().enabled = true;
					break;
				case 2:
					m_rogue.GetComponent<BS_Rogue>().enabled = true;
					break;
				case 3:
					m_hunter.GetComponent<BS_Hunter>().enabled = true;
					break;
				default:
					Debug.Log("Char not found");
					break;
			}
		}
	}
}
