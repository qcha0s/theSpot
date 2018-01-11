using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BS_Hunter : MonoBehaviour {
	
	public GameObject m_ArrowPrefab;
	public GameObject m_ArrowSpawn;
	public Transform m_ArrowHandler;
	public Image[] m_CDMasks;
	public GameObject m_Ultimate;
	public GameObject m_IceArrowPrefab;
	public GameObject m_FlameArrow;
	public BS_SoundManager m_soundMgr;

	private float m_ArrowVelocity = 500;
	private bool m_ultActive = true;
	private bool m_isUlting = false;
	private Animator m_animationController;
	private bool m_iceOnCD = false;
	private bool m_disOnCD = false;
	private float m_iceCD=6f;
	private float m_ultarrowcount = 20;
	private float m_DisCD = 10f;
	private Vector3 m_DisengageVec = new Vector3(0, 2f, -3f);
	private RPGCharacterController m_characterController;

	void Start () {
		m_characterController = GetComponent<RPGCharacterController>();
		m_animationController = GetComponent<Animator>();
		for(int i = 0; i < m_CDMasks.Length; i++){
			m_CDMasks[i].fillAmount = 0;
			Color temp = m_CDMasks[i].color;
			temp.a = 0.7f;
			m_CDMasks[i].color = temp;
		}
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			m_animationController.SetTrigger("isAttacking");
			ArrowFire();	
		}
		if(Input.GetKeyDown(KeyCode.Alpha1) && !m_iceOnCD) {
			IceArrow();
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && !m_disOnCD) {
			Disengage();
		}
	}

	void ArrowFire() {
		m_soundMgr.PlayArrowShot();
		var bullet = (GameObject)Instantiate (m_ArrowPrefab, m_ArrowHandler.position, m_ArrowHandler.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
		Destroy(bullet, 2.0f);
	}

	// Ice arrow does more damage than normal arrow
	void IceArrow() {
		m_soundMgr.PlayArrowShot();
		m_soundMgr.PlayIceImpact();
		var bullet = (GameObject)Instantiate (m_IceArrowPrefab, m_ArrowHandler.position, m_ArrowHandler.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 3;
		m_CDMasks[0].fillAmount = 1;
		m_iceOnCD = true;
		StartCoroutine(CoolDownSystem(m_iceCD,"Ice"));
	}

	// flame arrow  does damage over time to the target
	void Disengage() {
		m_soundMgr.PlayDisengage();
		m_animationController.SetBool("Movement", true);
		m_characterController.m_Disengage = true;
		m_CDMasks[1].fillAmount = 1;
		m_disOnCD = true;
		StartCoroutine(CoolDownSystem(m_DisCD, "Disengage"));
	}

	// ultimate create a volley of arrows that drop on enemies and do increased damage
	public void Ultimate() {
		m_animationController.SetBool("IsUlting", true);
		StartCoroutine(StartUlt());
	}

	IEnumerator StartUlt(){
		m_isUlting = true;
		while(m_isUlting){
			m_characterController.m_disableMovement = true;
			Vector3 bulletRotation = transform.rotation.eulerAngles;
			bulletRotation += new Vector3(0, Random.Range(-20, 20), 0);
			m_soundMgr.PlayArrowShot();
			var bullet = (GameObject)Instantiate (m_ArrowPrefab, m_ArrowHandler.position, Quaternion.Euler(bulletRotation));
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
			Destroy(bullet, 2.0f);
			m_ultarrowcount--;
			
			if(m_ultarrowcount == 0f){
				m_isUlting = false;
			}
			yield return new WaitForSeconds(0.1f);
		}
		m_ultarrowcount = 20;
		m_animationController.SetBool("IsUlting", false);
		m_characterController.m_disableMovement = false;
		yield return null;
	}
	
	IEnumerator CoolDownSystem(float cooldownvalue, string Ability){
			if(Ability == "Ice"){
			
			while(m_iceOnCD){
				m_CDMasks[0].fillAmount -= Time.deltaTime / cooldownvalue;
				if(m_CDMasks[0].fillAmount == 0){
					m_iceOnCD = false;
				}
				yield return null;
			}
		}
		
		if(Ability == "Disengage"){
			while(m_disOnCD){
				m_CDMasks[1].fillAmount -= Time.deltaTime / cooldownvalue;
				if(m_CDMasks[1].fillAmount == 0){
					m_disOnCD = false;
				}
				yield return null;
			}
		}
	}
}
