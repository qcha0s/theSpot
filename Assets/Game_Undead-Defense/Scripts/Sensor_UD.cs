using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_UD : MonoBehaviour {

	private List<BS_Health> m_targets = new List<BS_Health>();
	public List<BS_Health> Targets{ get{ return m_targets; }}

	private void OnTriggerEnter(Collider other) {	
		m_targets.Add(other.GetComponent<BS_Health>());
	}

	private void OnTriggerExit(Collider other) {
		m_targets.Remove(other.GetComponent<BS_Health>());
	}

	public void ClearTargets() {
		m_targets.Clear();
	}

	public BS_Health GetClosestEnemy() {
		BS_Health closestEnemy = m_targets[0];
		float closestDistance = GetDistance(closestEnemy.transform.position);
		for (int i = 1; i < m_targets.Count; i++) {
			float distToEnemy = GetDistance(m_targets[i].transform.position);
			if (distToEnemy < closestDistance) {
				closestDistance = distToEnemy;
				closestEnemy = m_targets[i];
			}
		}
		return closestEnemy;
	}

	// public BS_Health GetStrongestEnemy() {
	// 	BS_Health strongestEnemy = m_targets[0];
	// 	for (int i = 1; i < m_targets.Count; i++) {
	// 		float distToEnemy = GetDistance(m_targets[i].transform.position);
	// 		if (distToEnemy < closestDistance) {
	// 			closestDistance = distToEnemy;
	// 			strongestEnemy = m_targets[i];
	// 		}
	// 	}
	// 	return strongestEnemy;		
	// }

	public BS_Health GetFirstEnemy() {
		return m_targets[0];
	}

	public BS_Health GetLastEnemy() {
		return m_targets[m_targets.Count-1];
	}

	private float GetDistance(Vector3 target) {
		return Mathf.Abs((target - transform.position).magnitude);
	}
}
