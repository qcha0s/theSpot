using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHealth_UD : BaseHealth {

	private void Update() {
		if (CheckIfDead()) {
			Die();
		}
	}

	public override void Die() {
		GameManager_UD.instance.GameLose();
	}
}
