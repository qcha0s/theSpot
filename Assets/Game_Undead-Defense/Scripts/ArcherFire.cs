using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFire : MonoBehaviour {

	private Animator anim;
	private SphereCollider m_DamageBox;
	private bool m_readyToFire = true;
	private float m_fireRate = 1.5f;
	public Rigidbody m_arrow; 
	public Transform m_firePoint;

	void Start() {
		anim = GetComponent<Animator>();
		m_DamageBox = GetComponentInChildren<SphereCollider>();
		GetComponent<CharacterMovement_UD>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
			GetComponent<CharacterMovement_UD>().enabled = false;
			anim.SetTrigger("isReady");
			anim.SetTrigger("isAim");
			anim.SetTrigger("isShoot");
			
		}
	}

	void ShootArrow () {
		Rigidbody arrow = Instantiate(m_arrow, m_firePoint.position, Quaternion.identity) as Rigidbody;
		arrow.AddForce(m_firePoint.forward * 70, ForceMode.Impulse);
	}
}