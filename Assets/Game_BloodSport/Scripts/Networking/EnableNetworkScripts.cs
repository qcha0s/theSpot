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

	public GameObject m_overallMage;
	public GameObject m_overallWarrior;
	public GameObject m_overallRogue;
	public GameObject m_overallHunter;
	public NetworkAnimator m_netAnim;

	private int m_animCounter = 0;
	private int m_mageWarriorNum = 8;
	private int m_rogueHunterNum = 7;
	private Animator m_currAnimator;

	void Awake() {
		m_netAnim = this.GetComponent<NetworkAnimator>();
	}
	

	public override void OnStartLocalPlayer() {
		//m_netAnim.enabled = true;
			switch(m_chosenChar) {
				case 0:
					m_mage.GetComponent<BS_Mage>().enabled = true;
					m_mage.GetComponent<RPGCharacterController>().enabled = true;
					//m_currAnimator = m_mage.GetComponent<Animator>();
					m_animCounter = m_mageWarriorNum;
					//m_overallMage.SetActive(true);
					break;
				case 1:
					m_warrior.GetComponent<BS_Warrior>().enabled = true;
					m_warrior.GetComponent<RPGCharacterController>().enabled = true;
					//m_currAnimator = m_warrior.GetComponent<Animator>();
					m_animCounter = m_mageWarriorNum;
					//m_overallWarrior.SetActive(true);
					break;
				case 2:
					m_rogue.GetComponent<BS_Rogue>().enabled = true;
					m_rogue.GetComponent<RPGCharacterController>().enabled = true;
					//m_currAnimator = m_rogue.GetComponent<Animator>();
					m_animCounter = m_rogueHunterNum;
					//m_overallRogue.SetActive(true);
					break;
				case 3:
					m_hunter.GetComponent<BS_Hunter>().enabled = true;
					m_hunter.GetComponent<RPGCharacterController>().enabled = true;
					//m_currAnimator = m_hunter.GetComponent<Animator>();
					m_animCounter = m_rogueHunterNum;
					//m_overallHunter.SetActive(true);
					break;
				default:
					Debug.Log("Char not found");
					break;
			}
		SetAnimator();	
	}

	void Start() {
		m_currAnimator = GetComponent<Animator>();
	}

	void Update() {
		Debug.Log(m_currAnimator.gameObject.name);
		Debug.Log(m_netAnim == null);
	}

	public void SetAnimator() {
		for(int i = 0; i < m_animCounter; i++) {
			m_netAnim.SetParameterAutoSend(i, true);
		}
	}
}
