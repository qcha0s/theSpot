using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower_UD : MonoBehaviour {

	public enum FiringMode {First,Closest,Strongest,Farthest}
	public enum AmmoType {Arrow,Cannon,Tar}
	public float m_range = 50f;
	public float m_fireRate = 1f;
	public bool m_predictMovement = true;
	public FiringMode m_firingMode = FiringMode.First;
	public AmmoType m_ammo = AmmoType.Arrow;
	public Transform firePoint;

	private Sensor_UD m_sensor;
	private bool m_readyToFire = true;
	private BaseHealth m_target = null;

	private void Start() {
		m_sensor = GetComponentInChildren<Sensor_UD>();
		m_sensor.GetComponent<SphereCollider>().radius = m_range;
	}

	private void Update() {
		GetTarget();
		if (m_sensor.Targets.Count > 0 && m_readyToFire) {
			StartCoroutine(Shoot());
		}
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, m_range);
	}

	private GameObject GetProjectile() {
		GameObject projectile = null;
		switch (m_ammo) {
			case AmmoType.Arrow:
				projectile = PoolManager_UD.Instance.GetObject((int)UD_Objects.Arrow);
				SetProjectileVelocity(projectile.GetComponent<Bullet>());
			break;
			case AmmoType.Cannon:
				projectile = PoolManager_UD.Instance.GetObject((int)UD_Objects.CannonBall);
				SetProjectileVelocity(projectile.GetComponent<Bullet>());
			break;
			case AmmoType.Tar:
				firePoint.position = m_target.transform.position;
				projectile = PoolManager_UD.Instance.GetObject((int)UD_Objects.TarField);
			break;
			default:
				Debug.Log("Unknown ammo type");
				projectile = null;
			break;
		}
		return projectile;
	}

	private void GetTarget() {
		switch (m_firingMode) {
			case FiringMode.First:
				m_target = m_sensor.GetFirstEnemy();
			break;
			case FiringMode.Closest:
				m_target = m_sensor.GetClosestEnemy();
			break;
			case FiringMode.Strongest:
				m_target = m_sensor.GetStrongestEnemy();
			break;
			case FiringMode.Farthest:
				m_target = m_sensor.GetLastEnemy();
			break;
			default:
				Debug.Log("unknown firing mode");
			break;
		}
	}

	IEnumerator Shoot() {
		m_readyToFire = false;
		GameObject projectile = GetProjectile();
		projectile.transform.position = firePoint.position;
		projectile.SetActive(true);
		yield return new WaitForSeconds(m_fireRate);
		m_readyToFire = true;
	}

	private void SetProjectileVelocity(Bullet projectile) {
		Debug.Log(m_target == null);
		Vector3 direction = new Vector3(m_target.transform.position.x - firePoint.position.x,m_target.transform.position.y - firePoint.position.y,m_target.transform.position.z - firePoint.position.z);
		float travelTime = (Mathf.Pow(direction.x,2)+Mathf.Pow(direction.z,2))/Mathf.Pow(projectile.m_speed,2);
		travelTime = Mathf.Sqrt(travelTime);
		if (m_predictMovement) {
			Vector3 newEnemyPos = m_target.transform.position + (m_target.GetComponent<NavWaypointAI_UD>().Velocity*travelTime);
			direction = new Vector3(newEnemyPos.x - firePoint.position.x,newEnemyPos.y - firePoint.position.y,newEnemyPos.z - firePoint.position.z);
		}
		Vector3 newVelocity = new Vector3(direction.x/travelTime,(direction.y/travelTime)-(Physics.gravity.y*travelTime/2f),direction.z/travelTime);
		projectile.RBody.velocity = newVelocity;
	}

	private void OnEnable() {
		m_sensor.ClearTargets();
	}
}
