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
}
