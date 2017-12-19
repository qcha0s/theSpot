using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	public float m_maxDamage;
	public float m_minDamage;
	private List<BaseHealth> m_targets = new List<BaseHealth>();
	public List<BaseHealth> Targets{ get{ return m_targets; }}
	// private SphereCollider m_DamageBox;
	
	void Start() {
	// m_DamageBox = GetComponent<SphereCollider>();
	}

	void Update() {
	}
	
	private void OnTriggerEnter(Collider other) {
		Debug.Log("DMG");
		BaseHealth target = other.GetComponent<BaseHealth>();

		if(target != null) {
			m_targets.Add(other.GetComponent<BaseHealth>());
			target.TakeDamage(Random.Range(m_minDamage, m_maxDamage));
			
			Debug.Log(target.Health);
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			m_targets.Remove(other.GetComponent<BaseHealth>());
		}
	}
}




/* Add this code below to the character or enemy AI script.
	Then add them to the "Event" in their Attack animation.
	It's to turn on and off the weapon hitbox.
	Also m_DamageBox is the colliders.
	
	private void HitBoxOn() {
		Debug.Log("ON");
		m_DamageBox.enabled = true;
	}

	private void HitBoxOff() {
		Debug.Log("OFF");
		m_DamageBox.enabled = false;
	}	*/
