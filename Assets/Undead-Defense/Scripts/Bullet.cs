using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	public float splashRad;
	public float speed = 70f;
	public GameObject impactParticle;

	public void Seek (Transform _target) {
		target = _target;
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame) {
			HitTarget();
			return;
		}

		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}

	void HitTarget() {
		Debug.Log("Hit");
		GameObject partInst = (GameObject)Instantiate(impactParticle, transform.position, transform.rotation);
		Destroy(partInst, 2f);

		if (splashRad > 0f) {
			Explode();
		} else {
			Damage(target);
		}

		Destroy(gameObject);
	}

	void Explode () {
		Collider[] colliders = Physics.OverlapSphere(transform.position, splashRad);
		foreach(Collider collider in colliders) {
			if(collider.tag == "Enemy") {
				Damage(collider.transform);
			}
		}
	}

	void Damage(Transform enemy) {
		//Destroy(enemy.gameObject);

	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, splashRad);
	}

}
