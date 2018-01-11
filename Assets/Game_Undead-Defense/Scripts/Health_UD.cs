using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health_UD : BaseHealth {

	public bool m_isPlayer = false;
	public Image healthBar;

	void Update () {
		if (!m_isPlayer) {
			healthBar.fillAmount = Health/m_maxHealth;
			healthBar.color = Color.Lerp(m_deadColour, m_healthyColour, healthBar.fillAmount);
		}
	}

	public override void Die() {
		if (!m_isPlayer) {
			m_currentHealth = m_maxHealth;
			m_isDead = false;
			healthBar.color = m_healthyColour;
			gameObject.SetActive(false);
		}
	}

	public override void TakeDamage(float damage) {
		if (!m_isDead) {
			m_currentHealth -= damage;
			if (m_currentHealth <= 0) {
				m_currentHealth = 0;
			}
			if (m_isPlayer){
				GameManager_UD.instance.UpdatePlayerHPBar( m_currentHealth / m_maxHealth);
			}
		}
	}
}
