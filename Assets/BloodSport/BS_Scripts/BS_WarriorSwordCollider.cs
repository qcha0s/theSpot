using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_WarriorSwordCollider : MonoBehaviour {

	private float m_damage = 10.0f;

	void OnTriggerEnter(Collider other) {
		if(other.name == "Enemy") {
			BS_Health healthScript = other.GetComponent<BS_Health>();
			if(healthScript != null) {
				healthScript.TakeDamage(m_damage);
				Debug.Log(healthScript.m_currentHealth);
			}
		}
	}
}
