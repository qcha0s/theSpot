﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_WarriorSwordCollider : MonoBehaviour {

	public float m_normalDamage = 10.0f;
	public float m_whirlwindDamage = 40.0f;

	private RPGCharacterController m_rpgController;
	private BS_Warrior m_warriorController;
	private BS_Ultimate m_ultimateScript;

	void Start() {
		m_rpgController = transform.root.GetComponent<RPGCharacterController>();
		m_warriorController = transform.root.GetComponent<BS_Warrior>();
		m_ultimateScript = transform.root.GetComponent<BS_Ultimate>();
	}	

	void OnTriggerEnter(Collider other) {
		if(other.name == "Enemy" && !m_rpgController.m_hasDealtDamage) {
			BS_Health healthScript = other.GetComponent<BS_Health>();
			if(healthScript != null) {
				if(m_warriorController.m_usingWhirlWind) {
					healthScript.TakeDamage(m_whirlwindDamage * m_warriorController.m_damageMultiplier);
				} else {
					healthScript.TakeDamage(m_normalDamage * m_warriorController.m_damageMultiplier);
				}
				m_ultimateScript.Charge(m_warriorController.m_ultimateHitChargeAmount);
				m_rpgController.m_hasDealtDamage = true;
				Debug.Log(healthScript.m_currentHealth);
			}
		}
	}
}