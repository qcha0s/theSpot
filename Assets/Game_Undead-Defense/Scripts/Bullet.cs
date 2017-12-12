using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	private Rigidbody m_rb;

	public float m_splashRad = 0f;
	public Rigidbody RBody { get{ return m_rb;} }
	public float m_speed = 70f;
	public float m_damage = 5f;
	public float m_fadeOutTime = 1f;
	public GameObject impactParticle;

	private void Awake() {
		m_rb = GetComponent<Rigidbody>();
	}

	public void Seek (Transform _target) {
		target = _target;
	}
	
	void Update () {
		transform.rotation = Quaternion.LookRotation(m_rb.velocity);
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Hit");

		// NEED TO SETUP LAYER FOR ENEMIES SO PROJECTILES KNOW WHAT TO HIT

		// GameObject partInst = Instantiate(impactParticle, transform.position, transform.rotation);
		// Destroy(partInst, 2f);

		// if (m_splashRad > 0f) {
		// 	Explode();
		// } else {
		// 	BaseHealth targetHealth = other.GetComponent<BaseHealth>();
		// 	if (targetHealth != null) {
		// 		targetHealth.TakeDamage(m_damage);
		// 	}
		// }
//		m_rb.velocity = Vector3.zero;
//		StartCoroutine(Deactivate());
	}

	void Explode () {
		Collider[] colliders = Physics.OverlapSphere(transform.position, m_splashRad);
		foreach(Collider collider in colliders) {
			BaseHealth targetInRange = collider.GetComponent<BaseHealth>();
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
		// MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
		// Color32 meshColor = mesh.material.color;
		// float matAlpha = mesh.material.color.a;
		for (float i = 0; i < m_fadeOutTime; i+=Time.deltaTime) {
			// float newAplha = (matAlpha - (i/m_fadeOutTime)*matAlpha);
			// meshColor.a = (byte)newAplha;
			// mesh.material.color = meshColor;
			yield return null;
		}
		gameObject.SetActive(false);
		// meshColor.a = (byte)matAlpha;
		// mesh.material.color = meshColor;
	}
}
