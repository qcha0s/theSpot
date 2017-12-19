using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Rogue : MonoBehaviour {
	public Collider[] m_weaponHitBoxes;
	public GameObject m_targetGUI;
	public GameObject[] m_weapons;

	private bool m_UltActive = true;
	private bool m_sprintOnCD = false;
	private bool m_poisonOnCD = false;
	private float m_sprintCD=10f;
	private bool m_sprinting = false;
	private float m_poisonCD = 6f;
	private BS_Health m_healthScript;
	private RPGCharacterController m_characterController;
	private Animator m_animator;
	
	void Start () {
		m_animator = GetComponent<Animator>();
		m_healthScript = GetComponent<BS_Health>();
		m_characterController = GetComponent<RPGCharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Alpha1) && !m_poisonOnCD){
			Poison();
		}

		else if(Input.GetKey(KeyCode.Alpha2) && !m_sprintOnCD ){
			Sprint();
		}else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			m_targetGUI.SetActive(true);
		}
	}
	public void Sprint(){
		if(m_sprinting == false ){
				m_sprinting = true;
				m_sprintOnCD = true;
				StartCoroutine(CoolDownSystem(m_sprintCD,"Sprint"));
				StartCoroutine(StartSprint());
			} 
	}

	public void Poison(){
		m_weapons[0].GetComponent<RogueWeaponScript>().SetPoison(true);
		m_weapons[1].GetComponent<RogueWeaponScript>().SetPoison(true);
		m_poisonOnCD = true;
		StartCoroutine(CoolDownSystem(m_sprintCD,"Poison"));
		StartCoroutine(StartPoison());
	}

	public void ShadowStep(Transform targetLocation){
		gameObject.SetActive(false);
		gameObject.transform.position = targetLocation.position;
		gameObject.SetActive(true);
		m_targetGUI.SetActive(false);
	}
	IEnumerator StartSprint(){
		yield return new WaitForSeconds(4);
		
		
	}
	IEnumerator StartPoison(){
		yield return new WaitForSeconds(3);
		m_weapons[0].GetComponent<RogueWeaponScript>().SetPoison(false);
		m_weapons[1].GetComponent<RogueWeaponScript>().SetPoison(false);
	}
	IEnumerator CoolDownSystem(float cooldownvalue, string Ability){
		
		yield return new WaitForSeconds(cooldownvalue);
		if(Ability == "Sprint"){
			m_sprintOnCD = false;
		}
		if(Ability == "Poison"){
			m_poisonOnCD = false;
		}
		
	}
}
