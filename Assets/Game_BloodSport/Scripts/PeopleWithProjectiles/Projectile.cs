using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private int m_fireDmg = 60;

	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if(!(other.gameObject.name == "Mage") && this.gameObject.tag == "Player") {
			if(other.gameObject.tag == "Enemy") {
				other.GetComponent<BS_Health>().TakeDamage(m_fireDmg);
				Destroy(this.gameObject);
			}
		}
		if(!(other.gameObject.name == "Mage") && this.gameObject.tag == "Enemy") {
			if(other.gameObject.tag == "Player") {
				other.GetComponent<BS_Health>().TakeDamage(m_fireDmg);
				Destroy(this.gameObject);
			}
		}
		if(other.gameObject.tag == "pickup"){
			other.GetComponent<BS_Pickup>().TakeDamage(m_fireDmg);
			Destroy(this.gameObject);
		}
	}
}
