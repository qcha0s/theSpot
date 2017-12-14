using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_WarriorSwordCollider : MonoBehaviour {

	private float m_damage = 10.0f;
	private RPGCharacterController m_rpgController;

	void Start() {
		m_rpgController = transform.root.GetComponent<RPGCharacterController>();
	}

	void OnTriggerEnter(Collider other) {
		if(other.name == "Enemy" && !m_rpgController.m_hasDealtDamage) {
			BS_Health healthScript = other.GetComponent<BS_Health>();
			if(healthScript != null) {
				healthScript.TakeDamage(m_damage);
				m_rpgController.m_hasDealtDamage = true;
				Debug.Log(healthScript.m_currentHealth);
			}
		}
	}
}
