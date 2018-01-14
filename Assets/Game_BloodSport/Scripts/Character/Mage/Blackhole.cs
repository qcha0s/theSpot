using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour {

	public float m_pullRadius = 10.0f;
	public float m_pullForce = 200.0f;

	private bool m_hitEnemy = false;

	public void FixedUpdate() {
		foreach (Collider collider in Physics.OverlapSphere(transform.position, m_pullRadius)) {
			if(this.tag == "Player"){
				if(collider.gameObject.tag == "Enemy") {
					m_hitEnemy = true;
					this.GetComponent<Rigidbody>().velocity = Vector3.zero;
					// calculate the direction from the target
					Vector3 forceDirection = transform.position - collider.transform.position;

					// apply the force on target towards center
					collider.GetComponent<CharacterController>().Move(forceDirection * m_pullForce * Time.fixedDeltaTime);
				}
			}
			else if(this.tag == "Enemy"){
				if(collider.gameObject.tag == "Player") {
					m_hitEnemy = true;
					this.GetComponent<Rigidbody>().velocity = Vector3.zero;
					// calculate the direction from the target
					Vector3 forceDirection = transform.position - collider.transform.position;

					// apply the force on target towards center
					collider.GetComponent<CharacterController>().Move(forceDirection * m_pullForce * Time.fixedDeltaTime);
				}
			}
		}
	}

	public void Update() {
		if(m_hitEnemy) {
			// if you hit an enemy destroy the blackhole after 5 seconds
			Destroy(this.gameObject, 5.0f);
		}
	}
}
