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
	
	private Animator m_animationController;
	private bool m_PolyonCD;
	private bool m_BlinkOnCD;
	private float m_PolyCD = 6f;
	private float m_BlinkCD = 6f;
	// Use this for initialization
	void Start () {
		m_animationController = transform.root.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			m_animationController.SetTrigger("isAttacking");
			AnimationEvent ae = new AnimationEvent();
			ae.messageOptions = SendMessageOptions.DontRequireReceiver;
			Debug.Log("pressed mouse button");
		}
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			m_animationController.SetTrigger("isAbility1");
			Debug.Log("pressed 1");
			m_animationController.SetTrigger("isPoly");
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			m_animationController.SetTrigger("isAbility2");
			Debug.Log("pressed 2");
			m_animationController.SetTrigger("isBlinking");
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)) {
			m_animationController.SetTrigger("isAbility3");
			Debug.Log("pressed 3");
			m_animationController.SetTrigger("isUltimate");
		}
	}

	void Fire() {
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (fireBallPrefab, spellSpawn.position, spellSpawn.rotation);
		
		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

	void Blink() {
		transform.position += transform.rotation * Vector3.forward * 5;
	}

	void Polymorph() {
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (polyPrefab, spellSpawn.position, spellSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

	public void Ultimate() {
   	 	// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (ultimatePrefab, ultPos.position, ultPos.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
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
