using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArrow : MonoBehaviour {
	private int m_IceDamage = 100;
	public AudioClip m_IceHit;
	void OnCollisionEnter(Collision other) {
		if(!(other.gameObject.name == "Hunter")) {
			Destroy(this.gameObject);
			if(other.gameObject.GetComponent<RPGCharacterController>() != null){
				gameObject.GetComponent<AudioSource>().PlayOneShot(m_IceHit);
				other.gameObject.GetComponent<BS_Health>().TakeDamage(m_IceDamage);
				other.gameObject.GetComponent<RPGCharacterController>().StartCoroutine("SlowDown");
				
			}
		}
	}
}
