using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Health : MonoBehaviour {

	public float m_maxHealth = 1000.0f;
	public float m_currentHealth;
	public float m_damageReduction = 1f;
	public Slider m_healthBar;
	public Text m_healthText;

	void Start() {
		m_currentHealth = m_maxHealth;
		if(m_healthBar != null && m_healthText != null) {
			m_healthBar.value = CalculateHealth();
			m_healthText.text = m_currentHealth + "/" + m_maxHealth;
		}
	}

	public void TakeDamage(float damage) {
		m_currentHealth -= damage * m_damageReduction;
		Die();
		if(m_healthBar != null && m_healthText != null) {
			m_healthBar.value = CalculateHealth();
			m_healthText.text = m_currentHealth + "/" + m_maxHealth;
		}
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
		if(m_currentHealth > m_maxHealth) {
			m_currentHealth = m_maxHealth;
		}
		if(m_healthBar != null && m_healthText != null) {
			m_healthBar.value = CalculateHealth();
			m_healthText.text = m_currentHealth + "/" + m_maxHealth;
		}
	}

	private float CalculateHealth() {
		return m_currentHealth / m_maxHealth;
	}
}
