using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_WarriorShieldCollider : MonoBehaviour {

	public float m_damage = 60.0f;
	
	private BS_Warrior m_warriorScript;
	private BS_Ultimate m_ultimateScript;

	void Start() {
		m_warriorScript = transform.root.GetComponent<BS_Warrior>();
		m_ultimateScript = transform.root.GetComponent<BS_Ultimate>();
	}

	void OnTriggerEnter(Collider other) {
		if(!(other.name == "warrior")) {
			m_warriorScript.ResetAfterCharge();
			if(other.name == "Enemy") {
				BS_Health healthScript = other.GetComponent<BS_Health>();
				if(healthScript != null) {
					healthScript.TakeDamage(m_damage);
					m_ultimateScript.Charge(1);
					Debug.Log(healthScript.m_currentHealth);
				}
			}
		}
	}
}
