using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_WarriorSwordCollider : MonoBehaviour {

	public float m_normalDamage = 10.0f;
	public float m_whirlwindDamage = 40.0f;

	private RPGCharacterController m_rpgController;
	private BS_Warrior m_warriorController;
	private BS_Ultimate m_ultimateScript;
	private int DamageMultiplier;
	void Start() {
		m_rpgController = transform.GetComponentInParent<RPGCharacterController>();
		m_warriorController = transform.GetComponentInParent<BS_Warrior>();
		m_ultimateScript = transform.GetComponentInParent<BS_Ultimate>();
	}	
	void Update(){
         DamageMultiplier = m_rpgController.multiplier;
    }
	void OnTriggerEnter(Collider other) {
		if(other.name == "Enemy" && !m_rpgController.m_hasDealtDamage) {
			BS_Health healthScript = other.GetComponent<BS_Health>();
			if(healthScript != null) {
				if(m_warriorController.m_usingWhirlWind) {
					healthScript.TakeDamage(m_whirlwindDamage * DamageMultiplier);
				} else {
					healthScript.TakeDamage(m_normalDamage * DamageMultiplier);
				}
				m_ultimateScript.Charge(m_warriorController.m_ultimateHitChargeAmount);
				m_rpgController.m_hasDealtDamage = true;
			}
		}
	}
}
