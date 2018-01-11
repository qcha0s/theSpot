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
	public NetworkAnimator m_networkAnimator;

	void Start() {
		if(isLocalPlayer) {
			switch(m_chosenChar) {
				case 0:
					m_mage.GetComponent<BS_Mage>().enabled = true;
					m_mage.GetComponent<RPGCharacterController>().enabled = true;
					break;
				case 1:
					m_warrior.GetComponent<BS_Warrior>().enabled = true;
					m_warrior.GetComponent<RPGCharacterController>().enabled = true;
					break;
				case 2:
					m_rogue.GetComponent<BS_Rogue>().enabled = true;
					m_rogue.GetComponent<RPGCharacterController>().enabled = true;
					break;
				case 3:
					m_hunter.GetComponent<BS_Hunter>().enabled = true;
					m_hunter.GetComponent<RPGCharacterController>().enabled = true;
					break;
				default:
					Debug.Log("Char not found");
					break;
			}
		}
	}
}
