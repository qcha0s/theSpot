using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Health : MonoBehaviour {

	public const float MAX_HEALTH = 1000.0f;
	public float m_currentHealth;

	void start() {
		m_currentHealth = MAX_HEALTH;
	}

	public void TakeDamage(float damage) {
		m_currentHealth -= damage;
		Die();
	}

	public void Die() {
		if(m_currentHealth <= 0) {
			m_currentHealth = 0;
			// TODO: add what happens when they die
			Debug.Log("Dead");
		}
	}

	public void Heal(int amount) {
		m_currentHealth += amount;
		if(m_currentHealth > MAX_HEALTH) {
			m_currentHealth = MAX_HEALTH;
		}
	}
}
