using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Rigidbody m_rb;
	public float m_splashRad = 0f;
	public Rigidbody RBody { get{ return m_rb;} }
	public float m_speed = 70f;
	public float m_damage = 5f;
	public float m_fadeOutTime = 20f;
	public GameObject impactParticle;

	private void Awake() {
		m_rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		transform.rotation = Quaternion.LookRotation(m_rb.velocity);
	}

	private void OnTriggerEnter(Collider other) {
		// GameObject partInst = Instantiate(impactParticle, transform.position, transform.rotation);
		// Destroy(partInst, 2f);
		if (m_splashRad > 0f) {
			Explode();
		} else {
			BaseHealth targetHealth = other.GetComponent<BaseHealth>();
			if (targetHealth != null) {
				targetHealth.TakeDamage(m_damage);
			}
		}
		gameObject.SetActive(false);
//		StartCoroutine(Deactivate());
	}

	void Explode () {
		Collider[] colliders = Physics.OverlapSphere(transform.position, m_splashRad);
		for (int i = 0; i < colliders.Length; i++) {
			Debug.Log("hit " + colliders[i].gameObject.name);
			BaseHealth targetInRange = colliders[i].GetComponent<BaseHealth>();
			if (targetInRange != null) {
				targetInRange.TakeDamage(m_damage);
			}
		}
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, m_splashRad);
	}

	IEnumerator Deactivate() {
		for (float i = 0; i < m_fadeOutTime; i+=Time.deltaTime) {
			yield return null;
		}
		gameObject.SetActive(false);
	}
}
