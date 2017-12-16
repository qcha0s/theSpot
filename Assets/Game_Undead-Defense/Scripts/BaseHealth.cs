using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour {

	public float m_maxHealth = 100f;
	public bool IsDead { get{ return m_isDead; }}
	public float Health {get{ return m_currentHealth; }}

	private float m_currentHealth;
	private bool m_isDead = false;

	private void Start() {
		m_currentHealth = m_maxHealth;
	}

	public virtual void TakeDamage(float damage) {
		if (!m_isDead) {
			m_currentHealth -= damage;
			if (m_currentHealth <= 0) {
				m_currentHealth = 0;
			}
		}
	}

	public bool CheckIfDead() {
		if (m_currentHealth <= 0) {
			m_isDead = true;
		}
		return m_isDead;
	}

	private void OnDisable() {
		m_currentHealth = m_maxHealth;
		m_isDead = false;
	}

	public abstract void Die();
}
