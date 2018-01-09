using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public AudioClip m_ArrowHit;
	private float m_fireDamage = 5;
	private float m_fireCount = 4;
	private float m_totalFireTime = 2;
	private float m_ArrowDamage=40;
	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "Warrior"){
			other.gameObject.GetComponent<RPGCharacterController>().m_soundMgr.PlayArmorStrike();
		}
		BS_Health healthScript = other.GetComponent<BS_Health>();
		if(healthScript != null) {
			gameObject.GetComponent<AudioSource>().PlayOneShot(m_ArrowHit);
			other.gameObject.GetComponent<BS_Health>().TakeDamage(m_ArrowDamage);
			other.gameObject.GetComponent<BS_Health>().TakeDotDamage(m_fireDamage,m_fireCount,m_totalFireTime);
		}
		Destroy(gameObject);
	}
}
