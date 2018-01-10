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
	public Image[] m_CDMasks;
	public BS_SoundManager m_soundMgr;
	public bool m_ultActive = false;

	private Animator m_animationController;
	private bool m_PolyonCD;
	private bool m_BlinkOnCD;
	private float m_PolyCD = 6f;
	private float m_BlinkCD = 6f;

	// Use this for initialization
	void Start () {
		m_animationController = GetComponent<Animator>();
		for(int i = 0; i < m_CDMasks.Length;i++){
			m_CDMasks[i].fillAmount=0;
			Color temp = m_CDMasks[i].color;
			temp.a=0.7f;
			m_CDMasks[i].color=temp;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			m_animationController.SetTrigger("isAttacking");
		}
		if(Input.GetKeyDown(KeyCode.Alpha1) && !m_PolyonCD) {
			m_animationController.SetTrigger("isAbility1");
			m_animationController.SetTrigger("isPoly");
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && !m_BlinkOnCD) {
			m_animationController.SetTrigger("isAbility2");
			m_animationController.SetTrigger("isBlinking");
		}
		if(Input.GetKeyDown(KeyCode.Alpha3) && !m_ultActive) {
			m_animationController.SetTrigger("isAbility3");
			m_animationController.SetTrigger("isUltimate");
		}
	}

	void Fire() 	{
		//m_soundMgr.PlayFire();
    	// Create the Bullet from the Bullet Prefab
    	var bullet = (GameObject)Instantiate (fireBallPrefab, spellSpawn.position, spellSpawn.rotation);

    	// Add velocity to the bullet
	 	bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

	void Blink() {
		//m_soundMgr.PlayBlink();
		transform.position += transform.rotation * Vector3.forward * 5;
		m_BlinkOnCD = true;
		m_CDMasks[1].fillAmount = 1;
		StartCoroutine(CoolDownSystem(m_BlinkCD,"Blink"));

	}

	void Polymorph() {

		//m_soundMgr.PlayBlackhole();
		//m_soundMgr.PlayPoly();
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (polyPrefab, spellSpawn.position, spellSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
		m_PolyonCD = true;
		m_CDMasks[0].fillAmount = 1;
		StartCoroutine(CoolDownSystem(m_PolyCD,"Poly"));
		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
		
	}

	public void Ultimate() 	{
		//m_soundMgr.PlayBlackhole();
		//m_soundMgr.PlayBlackhole();
    	// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (ultimatePrefab, ultPos.position, ultPos.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
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
