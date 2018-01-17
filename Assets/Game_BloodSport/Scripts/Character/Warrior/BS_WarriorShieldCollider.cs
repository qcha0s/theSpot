using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_WarriorShieldCollider : MonoBehaviour {

	public float m_damage = 60.0f;
	
	private BS_Warrior m_warriorController;
	private BS_Ultimate m_ultimateScript;
	private RPGCharacterController m_rpgController;
	private int DamageMultiplier;
	void Start() {
		m_warriorController = transform.root.GetComponent<BS_Warrior>();
		m_ultimateScript = transform.root.GetComponent<BS_Ultimate>();
	}
	void Update(){
         DamageMultiplier = m_rpgController.multiplier;
    }
	void OnTriggerEnter(Collider other) {
		if(!(other.name == "warrior")) {
			m_warriorController.ResetAfterCharge();
			if(other.name == "Enemy") {
				BS_Health healthScript = other.GetComponent<BS_Health>();
				if(healthScript != null) {
					healthScript.TakeDamage(m_damage * DamageMultiplier);
					m_ultimateScript.Charge(m_warriorController.m_ultimateHitChargeAmount);
					Debug.Log(healthScript.m_currentHealth);
				}
			}
		}
	}
}
