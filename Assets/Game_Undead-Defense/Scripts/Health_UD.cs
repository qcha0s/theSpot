using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_UD : BaseHealth {

	public bool m_isPlayer = false;

	public override void Die() {
		if (!m_isPlayer) {
			gameObject.SetActive(false);
		}
	}
}
