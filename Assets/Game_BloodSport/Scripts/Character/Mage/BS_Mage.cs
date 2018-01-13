using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Mage : MonoBehaviour {

	public GameObject fireBallPrefab;
	public GameObject polyPrefab;
	public GameObject ultimatePrefab;
	public Transform spellSpawn;
	public Transform ultPos;
	public PolyManager m_polyScript;
	public bool m_isMe;
	public Image[] m_CDMasks;
	public BS_SoundManager m_soundMgr;
	public bool m_ultActive = false;

	private RPGCharacterController m_characterController;
	private Animator m_animationController;
	private bool m_PolyonCD;
	private bool m_BlinkOnCD;
	private float m_PolyCD = 6f;
	private float m_BlinkCD = 6f;
	private NetworkedMage networkedMage;
	// Use this for initialization
	void Start () {
		m_animationController = transform.root.GetComponent<Animator>();
		m_characterController = GetComponent<RPGCharacterController>();
		for(int i = 0; i < m_CDMasks.Length; i++){
			m_CDMasks[i].fillAmount = 0;
			Color temp = m_CDMasks[i].color;
			temp.a = m_characterController.m_cdTransparency;
			m_CDMasks[i].color = temp;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if(!m_isMe) {
			if(other.gameObject.name == "Polymorph(Clone)") {
				m_polyScript.SetPoly();
			}
		}
	}

	IEnumerator CoolDownSystem(float cooldownvalue, string Ability) {
		if(Ability == "Poly") {
		
			while(m_PolyonCD) {
				m_CDMasks[0].fillAmount -= Time.deltaTime / cooldownvalue;

				if(m_CDMasks[0].fillAmount == 0){
					m_PolyonCD = false;
				}
				yield return null;
			}
		}
	
		if(Ability == "Blink"){
			while(m_BlinkOnCD){
				m_CDMasks[1].fillAmount -= Time.deltaTime / cooldownvalue;

				if(m_CDMasks[1].fillAmount == 0) {
					m_BlinkOnCD = false;
				}
				yield return null;
			}
		}
	}
}
