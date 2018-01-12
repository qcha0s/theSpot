using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarField : MonoBehaviour {

	public float slowEffect = 0.5f;

	void Start () {
		
	}
	
	// void Update () {
	// 	Shoot();
	// }

	// void Shoot () {
	// 	GameObject enemy = GameObject.Find("Enemy");
	// 	NavWaypointAI_UD enemyMove = enemy.GetComponent<NavWaypointAI_UD>();
	// 	enemyMove.Slow(slowEffect);
	// }

	private void OnTriggerStay(Collider other) {
		if (other.gameObject.activeInHierarchy) {
			NavWaypointAI_UD targetSpeed = other.GetComponent<NavWaypointAI_UD>();
			if (targetSpeed != null) {
				targetSpeed.Slow(slowEffect);
			}
		}
	}
}
