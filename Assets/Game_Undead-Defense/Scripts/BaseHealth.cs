using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour {

	public float m_maxHealth = 100f;
	public Color m_healthyColour;
	public Color m_deadColour;
	public bool IsDead { get{ return m_isDead; }}
	public float Health {get{ return m_currentHealth; }}

	protected float m_currentHealth;
	protected bool m_isDead = false;

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

	public abstract void Die();
}
