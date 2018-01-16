using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_UD : BaseHealth {

	private SoundManager_UD m_sound;

	public bool m_isPlayer = false;

	public override void Die() {
		if (!m_isPlayer) {
			m_currentHealth = m_maxHealth;
			m_isDead = false;
			gameObject.SetActive(false);
		}
	}
}

