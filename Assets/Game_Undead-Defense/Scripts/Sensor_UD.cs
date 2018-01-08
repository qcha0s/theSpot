using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_UD : MonoBehaviour {

	public bool m_isEnemy = false;
	[HideInInspector]
	public BaseHealth m_playerBase = null;

	private List<BaseHealth> m_targets = new List<BaseHealth>();
	public List<BaseHealth> Targets{ get{ return m_targets; }}
	private SphereCollider m_cldr;

	private void Start() {
		m_cldr = GetComponent<SphereCollider>();
	}

	private void Update() {
		RemoveDeactivatedEnemies();
	}

	private void OnTriggerEnter(Collider other) {
		BaseHealth newTarget = other.GetComponent<BaseHealth>();
		if (!m_isEnemy) {
			if (newTarget.gameObject.activeInHierarchy && !newTarget.IsDead) {
				m_targets.Add(newTarget);
			}
		} else {
			if (other.tag == "Player") {
				m_targets.Add(newTarget);
			} else if (other.tag == "PlayerBase") {
				m_playerBase = newTarget;
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		BaseHealth exitEnemy = other.GetComponent<BaseHealth>();
		m_targets.Remove(exitEnemy);
		// if (exitEnemy == m_playerBase) {
		// 	m_playerBase = null;
		// }
	}

	public void ClearTargets() {
		m_targets.Clear();
	}

	public BaseHealth GetClosestEnemy() {
		BaseHealth closestEnemy = null;
		float closestDistance = m_cldr.radius;
		for (int i = 0; i < m_targets.Count; i++) {
			if (m_targets[i].gameObject.activeInHierarchy && !m_targets[i].IsDead) {
				float distToEnemy = GetDistance(m_targets[i].transform.position);
				if (distToEnemy < closestDistance) {
					closestDistance = distToEnemy;
					closestEnemy = m_targets[i];
				}
			}
		}
		return closestEnemy;
	}

	public BaseHealth GetStrongestEnemy() {
		BaseHealth strongestEnemy = null;
		float highestHealth = 0;
		for (int i = 0; i < m_targets.Count; i++) {
			if (m_targets[i].gameObject.activeInHierarchy && !m_targets[i].IsDead) {
				float newEnemyHealth = m_targets[i].m_maxHealth;
				if (highestHealth < newEnemyHealth) {
					highestHealth = newEnemyHealth;
					strongestEnemy = m_targets[i];
				}
			}
		}
		return strongestEnemy;		
	}

	public BaseHealth GetFirstEnemy() {
		BaseHealth firstEnemy = null;
		for (int i = 0; i < m_targets.Count;) {
			if (m_targets[i].gameObject.activeInHierarchy && !m_targets[i].IsDead) {
				firstEnemy = m_targets[i];
				break;
			} else {
				m_targets.Remove(m_targets[i]);
			}
		}
		return firstEnemy;
	}

	public BaseHealth GetLastEnemy() {
		BaseHealth lastEnemy = null;
		while (lastEnemy == null && m_targets.Count > 0) {
			int i = m_targets.Count - 1;
			if (m_targets[i].gameObject.activeInHierarchy && !m_targets[i].IsDead) {
				lastEnemy = m_targets[i];
				break;
			} else {
				m_targets.Remove(m_targets[i]);
			}
		}
		return lastEnemy;
	}

	private float GetDistance(Vector3 target) {
		return Mathf.Abs((target - transform.position).magnitude);
	}

	private void RemoveDeactivatedEnemies () {
		for (int i = m_targets.Count-1; i > 0; i--) {
			if (!m_targets[i].gameObject.activeInHierarchy) {
				m_targets.Remove(m_targets[i]);
			}
		}
	}

	public void RessetSensor() {
		ClearTargets();
		m_playerBase = null;
	}
}
