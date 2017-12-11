using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour {

	public float m_maxHealth = 100f;
	public bool IsDead { get{ return m_isDead; }}

	private float m_currentHealth;
	private bool m_isDead = false;

	private void Start() {
		m_currentHealth = m_maxHealth;
	}

	public virtual void TakeDamage(float damage) {
		m_currentHealth -= damage;
		if (m_currentHealth <= 0) {
			m_currentHealth = 0;
			if (!m_isDead) {
				m_isDead = true;
				Die();
			}
		}
	}

	public abstract void Die();
}
