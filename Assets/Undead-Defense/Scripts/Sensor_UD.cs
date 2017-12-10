using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_UD : MonoBehaviour {

	private List<BaseHealth> m_targets = new List<BaseHealth>();
	public List<BaseHealth> Targets{ get{ return m_targets; }}

	private void OnTriggerEnter(Collider other) {
		BaseHealth newTarget = other.GetComponent<BaseHealth>();
		if (newTarget != null) {
			m_targets.Add(other.GetComponent<BaseHealth>());
		}
	}

	private void OnTriggerExit(Collider other) {
		m_targets.Remove(other.GetComponent<BaseHealth>());
	}

	public void ClearTargets() {
		m_targets.Clear();
	}

	public BaseHealth GetClosestEnemy() {
		BaseHealth closestEnemy = m_targets[0];
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

	public BaseHealth GetStrongestEnemy() {
		BaseHealth strongestEnemy = m_targets[0];
		float highestHealth = strongestEnemy.m_maxHealth;
		for (int i = 1; i < m_targets.Count; i++) {
			float newEnemyHealth = m_targets[i].m_maxHealth;
			if (highestHealth < newEnemyHealth) {
				highestHealth = newEnemyHealth;
				strongestEnemy = m_targets[i];
			}
		}
		return strongestEnemy;		
	}

	public BaseHealth GetFirstEnemy() {
		return m_targets[0];
	}

	public BaseHealth GetLastEnemy() {
		return m_targets[m_targets.Count-1];
	}

	private float GetDistance(Vector3 target) {
		return Mathf.Abs((target - transform.position).magnitude);
	}
}
