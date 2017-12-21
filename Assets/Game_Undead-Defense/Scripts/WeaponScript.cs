using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	public float m_maxDamage;
	public float m_minDamage;
	private List<BaseHealth> m_targets = new List<BaseHealth>();
	public List<BaseHealth> Targets{ get{ return m_targets; }}
	
	private void OnTriggerEnter(Collider other) {
		Debug.Log("DMG");
		BaseHealth target = other.GetComponent<BaseHealth>();
		if(target != null) {
			if (!m_targets.Contains(target)) {
				m_targets.Add(target);
				target.TakeDamage(Random.Range(m_minDamage, m_maxDamage));
				Debug.Log(target.Health);
			}
		}
	}

	public void Clear() {
		m_targets.Clear();
	}
}
