using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnableNetworkScripts : NetworkBehaviour {

	private int m_chosenChar = 0; //may need to change to public
	public GameObject m_mage;
	public GameObject m_warrior;
	public GameObject m_rogue;
	public GameObject m_hunter;
	public GameObject HunterAnim;
	public GameObject MageAnim;
	public GameObject RogueAnim;
	public GameObject WarriorAnim;
	public GameObject m_overallMage;
	public GameObject m_overallWarrior;
	public GameObject m_overallRogue;
	public GameObject m_overallHunter;
	public GameObject m_characterSelect;
	public NetworkAnimator m_netAnim;
	
	private int m_animCounter = 0;
	private int m_mageWarriorNum = 8;
	private int m_rogueHunterNum = 7;
	private Animator m_currAnimator;
	private Animator m_classAnimator;

	void Awake() {
		m_netAnim = this.GetComponent<NetworkAnimator>();
	}
	
	public override void OnStartLocalPlayer() {
		this.gameObject.SetActive(true);
		m_characterSelect.SetActive(true);
		//m_netAnim.enabled = true;
		
	}
	public void PickCharacter(int character){
		switch(m_chosenChar) {
			case 0:
				m_mage.GetComponent<BS_Mage>().enabled = true;
				m_mage.GetComponent<RPGCharacterController>().enabled = true;
				//m_currAnimator = m_mage.GetComponent<Animator>();
				m_animCounter = m_mageWarriorNum;
				EnableCharacter(0);
				break;
			case 1:
				m_warrior.GetComponent<BS_Warrior>().enabled = true;
				m_warrior.GetComponent<RPGCharacterController>().enabled = true;
				//m_currAnimator = m_warrior.GetComponent<Animator>();
				m_animCounter = m_mageWarriorNum;
				EnableCharacter(1);
				//m_overallWarrior.SetActive(true);
				break;
			case 2:
				m_rogue.GetComponent<BS_Rogue>().enabled = true;
				m_rogue.GetComponent<RPGCharacterController>().enabled = true;
				//m_currAnimator = m_rogue.GetComponent<Animator>();
				m_animCounter = m_rogueHunterNum;
				EnableCharacter(2);
				//m_overallRogue.SetActive(true);
				break;
			case 3:
				m_hunter.GetComponent<BS_Hunter>().enabled = true;
				m_hunter.GetComponent<RPGCharacterController>().enabled = true;
				//m_currAnimator = m_hunter.GetComponent<Animator>();
				m_animCounter = m_rogueHunterNum;	
				EnableCharacter(3);
				//m_overallHunter.SetActive(true);
				break;
			default:
				Debug.Log("Char not found");
				break;
		}
		m_characterSelect.SetActive(false);
		SetAnimator();	
	}
	public void EnableCharacter(int character) {
		m_currAnimator = GetComponent<Animator>();
		switch(m_chosenChar) {
			case 0:
				m_currAnimator.runtimeAnimatorController = Resources.Load("MageAnimationController") as RuntimeAnimatorController;
				m_classAnimator = MageAnim.GetComponent<Animator>();
				m_currAnimator.avatar = m_classAnimator.avatar;
				m_overallMage.SetActive(true);
				break;
			 case 1:
				m_currAnimator.runtimeAnimatorController = Resources.Load("WarriorAnimationController") as RuntimeAnimatorController;
				m_classAnimator = WarriorAnim.GetComponent<Animator>();
				m_currAnimator.avatar = m_classAnimator.avatar;
				m_overallWarrior.SetActive(true);
				break;
			case 2:
				m_currAnimator.runtimeAnimatorController = Resources.Load("RogueAnimationController") as RuntimeAnimatorController;
				m_classAnimator = RogueAnim.GetComponent<Animator>();
				m_currAnimator.avatar = m_classAnimator.avatar;
				m_overallRogue.SetActive(true);
				break;
			case 3:
				m_currAnimator.runtimeAnimatorController = Resources.Load("HunterAnimationController") as RuntimeAnimatorController;
				m_classAnimator = HunterAnim.GetComponent<Animator>();
				m_currAnimator.avatar = m_classAnimator.avatar;
				m_overallHunter.SetActive(true);
				break;
			default:
				m_currAnimator.runtimeAnimatorController = Resources.Load("MageAnimationController") as RuntimeAnimatorController;
				m_classAnimator = MageAnim.GetComponent<Animator>();
				m_currAnimator.avatar = m_classAnimator.avatar;
				m_overallMage.SetActive(true);
				break;
		}
	}

	public void SetAnimator() {
		for(int i = 0; i < m_animCounter; i++) {
			m_netAnim.SetParameterAutoSend(i, true);
			
		}
	}
}
