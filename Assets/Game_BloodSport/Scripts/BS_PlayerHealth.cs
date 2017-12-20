using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_PlayerHealth : MonoBehaviour {

	// Use this for initialization
	public const float PLAYER_MAX_HEALTH = 1000.0f;

	public float m_playerCurrentHealth;
	public int amount = 100;
	void start() {
		
		m_playerCurrentHealth = PLAYER_MAX_HEALTH;
	}

	public void TakeDamage(float damage) {
		m_playerCurrentHealth -= damage;
		Die();
	}

	public void Die() {
		if(m_playerCurrentHealth <= 0) {
			m_playerCurrentHealth = 0;
			Destroy(this);

			// TODO: add what happens when they die
			Debug.Log("Dead");
		}
	}

	public void PlayerHeal(int amount) {
		m_playerCurrentHealth += amount;
		if(m_playerCurrentHealth > PLAYER_MAX_HEALTH) {
			m_playerCurrentHealth = PLAYER_MAX_HEALTH;
		}
	}
}
