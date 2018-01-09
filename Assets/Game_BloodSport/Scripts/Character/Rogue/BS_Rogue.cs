using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Rogue : MonoBehaviour {
	public Collider[] m_weaponHitBoxes;
	public GameObject m_targetGUI;
	public GameObject[] m_weapons;
	public Image[] m_CDMasks;
	public Material m_poisonMats;
	public Material m_daggerBase;
	public BS_SoundManager m_soundMgr;
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
		for(int i = 0; i < m_CDMasks.Length;i++){
			m_CDMasks[i].fillAmount=0;
			Color temp = m_CDMasks[i].color;
			temp.a=0.7f;
			m_CDMasks[i].color=temp;
		}
	}
	
	// Update is called once per frame
	void Update () {

		
		if(m_characterController.m_walkByDefault == false){
			m_animator.SetBool("IsSprinting",true);
		}
		else{
			m_animator.SetBool("IsSprinting",false);
		}
		if(Input.GetMouseButtonDown(0)){
			m_animator.SetBool("isAttacking", true);
			m_weaponHitBoxes[0].enabled = true;
			m_weaponHitBoxes[1].enabled = true;
		}
		if(Input.GetMouseButtonDown(0)&& !m_animator.GetBool("isAttacking")){
			m_soundMgr.PlayDaggerSwipe();
		}

		if(Input.GetKey(KeyCode.Alpha1) && !m_poisonOnCD){
			m_soundMgr.PlayPoison();
			Poison();
		}

		 if(Input.GetKey(KeyCode.Alpha2) && !m_sprintOnCD ){
			Sprint();
		}else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			m_soundMgr.PlaySmoke();
			m_targetGUI.SetActive(true);
		}
	}
	public void Sprint(){
				m_sprintOnCD = true;
				m_CDMasks[1].fillAmount=1;
				StartCoroutine(CoolDownSystem(m_sprintCD,"Sprint"));
				StartCoroutine(StartSprint());
			
	}

	public void Poison(){
		m_weapons[0].GetComponent<MeshRenderer>().material = m_poisonMats;
		m_weapons[1].GetComponent<MeshRenderer>().material = m_poisonMats;
		m_weapons[0].GetComponent<RogueWeaponScript>().SetPoison(true);
		m_weapons[1].GetComponent<RogueWeaponScript>().SetPoison(true);
		m_CDMasks[0].fillAmount=1;
		m_poisonOnCD = true;
		StartCoroutine(CoolDownSystem(m_poisonCD,"Poison"));
		StartCoroutine(StartPoison());
		
		
	}

	public void ShadowStep(Transform targetLocation){
		gameObject.SetActive(false);
		gameObject.transform.position = targetLocation.position;
		gameObject.SetActive(true);
		m_targetGUI.SetActive(false);
	}
	IEnumerator StartSprint(){
		m_characterController.m_walkByDefault=false;
		yield return new WaitForSeconds(4);
		m_characterController.m_walkByDefault=true;
		
		
	}
	IEnumerator StartPoison(){

		yield return new WaitForSeconds(3);
		m_weapons[0].GetComponent<MeshRenderer>().material = m_daggerBase;
		m_weapons[1].GetComponent<MeshRenderer>().material = m_daggerBase;
		m_weapons[0].GetComponent<RogueWeaponScript>().SetPoison(false);
		m_weapons[1].GetComponent<RogueWeaponScript>().SetPoison(false);
	}
	IEnumerator CoolDownSystem(float cooldownvalue, string Ability){
			if(Ability == "Poison"){
			Debug.Log("StartCD");
			while(m_poisonOnCD){
				m_CDMasks[0].fillAmount-=Time.deltaTime/cooldownvalue;

				if(m_CDMasks[0].fillAmount==0){
					m_poisonOnCD = false;
				}
				yield return null;
				
			}
		}
		
		if(Ability == "Sprint"){
			while(m_sprintOnCD){
				m_CDMasks[1].fillAmount-=Time.deltaTime/cooldownvalue;

				if(m_CDMasks[1].fillAmount==0){
					m_sprintOnCD = false;
				}
				yield return null;
				
			}
			
		}
	
		
	}
}
