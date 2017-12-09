using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_Tower : MonoBehaviour {

	
	public Transform target;
	public float range = 15f;
	public string enemyTag = "Enemy";
	public Transform targetAim;
	public float fireRate = 1f;
	public GameObject bulletPrefab;
	public Transform firePoint;
	private float fireCountdown = 0f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null)
			return;

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotate = Quaternion.LookRotation(dir);
		Vector3 rotation = lookRotate.eulerAngles;
		targetAim.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if(fireCountdown <= 0f) {
			Shoot();

			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
	}

	void Shoot () {
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
//		Bullet bullet = bulletGO.GetComponent<Bullet>();

		// if(bullet != null) {
		// 	bullet.Seek(target);
		// }
	}

	void LookAtTarget () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortestDistance <= range) {
			target = nearestEnemy.transform;
		} else {
			target = null;
		}
	}

	void OnTriggerStay (Collider other) {
		// Debug.Log("Got you in my sights");
		InvokeRepeating("LookAtTarget", 0f, 0f);
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
