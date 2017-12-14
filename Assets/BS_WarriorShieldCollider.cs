using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_WarriorShieldCollider : MonoBehaviour {

	private float m_damage = 60.0f;
	private BS_Warrior m_warriorScript;

	void Start() {
		m_warriorScript = transform.root.GetComponent<BS_Warrior>();
	}

	void OnTriggerEnter(Collider other) {
		if(!(other.name == "warrior")) {
			//m_warriorScript.ResetAfterCharge();
			if(other.name == "Enemy") {
				BS_Health healthScript = other.GetComponent<BS_Health>();
				if(healthScript != null) {
					healthScript.TakeDamage(m_damage);
					Debug.Log(healthScript.m_currentHealth);
				}
			}
		}
	}
}
