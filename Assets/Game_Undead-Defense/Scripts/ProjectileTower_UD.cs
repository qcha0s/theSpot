using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower_UD : MonoBehaviour {

	public enum TowerType {ArrowTower,CannonTower,TarCauldron}
	public enum FiringMode {First,Closest,Strongest,Farthest}
	public enum AmmoType {Arrow,Cannon,Tar}
	public float m_range = 50f;
	public float m_fireRate = 1f;
	public float m_rotationSpeed = 30f;
	public bool m_predictMovement = true;
	public TowerType m_towerType = TowerType.ArrowTower;
	public FiringMode m_firingMode = FiringMode.First;
	public AmmoType m_ammo = AmmoType.Arrow;
	public Transform m_firePoint;
	public Transform m_turret;

	private Sensor_UD m_sensor;
	private bool m_facingTarget = false;
	private bool m_readyToFire = true;
	private BaseHealth m_target = null;

	private void Start() {
		m_sensor = GetComponentInChildren<Sensor_UD>();
		m_sensor.GetComponent<SphereCollider>().radius = m_range;
	}

	private void Update() {
		if (GetTarget() != null) {
			SetTurretRotation();
			if (m_readyToFire) {
				StartCoroutine(Shoot());
			}
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
				m_firePoint.position = m_target.transform.position;
				projectile = PoolManager_UD.Instance.GetObject((int)UD_Objects.TarField);
			break;
			default:
				Debug.Log("Unknown ammo type");
				projectile = null;
			break;
		}
		return projectile;
	}

	private BaseHealth GetTarget() {
		BaseHealth target = null;
		switch (m_firingMode) {
			case FiringMode.First:
				target = m_sensor.GetFirstEnemy();
			break;
			case FiringMode.Closest:
				target = m_sensor.GetClosestEnemy();
			break;
			case FiringMode.Strongest:
				target = m_sensor.GetStrongestEnemy();
			break;
			case FiringMode.Farthest:
				target = m_sensor.GetLastEnemy();
			break;
			default:
				Debug.Log("unknown firing mode");
			break;
		}
		m_target = target;
		return target;
	}

	IEnumerator Shoot() {
		m_readyToFire = false;
		GameObject projectile = GetProjectile();
		projectile.transform.position = m_firePoint.position;
		projectile.SetActive(true);
		yield return new WaitForSeconds(m_fireRate);
		m_readyToFire = true;
	}

	private void SetProjectileVelocity(Bullet projectile) {
		Vector3 direction = m_target.transform.position - m_firePoint.position;
		float travelTime = (Mathf.Pow(direction.x,2)+Mathf.Pow(direction.z,2))/Mathf.Pow(projectile.m_speed,2);
		travelTime = Mathf.Sqrt(travelTime);
		if (m_predictMovement) {
			Vector3 newEnemyPos = m_target.transform.position + (m_target.GetComponent<NavWaypointAI_UD>().Velocity*travelTime);
			direction = newEnemyPos - m_firePoint.position;
		}
		Vector3 newVelocity = new Vector3(direction.x/travelTime,(direction.y/travelTime)-(Physics.gravity.y*travelTime/2f),direction.z/travelTime);
		projectile.RBody.velocity = newVelocity;
	}

	private void OnEnable() {
		if (m_sensor != null) {
			m_sensor.ClearTargets();
		}
	}

	private void SetTurretRotation() {
		switch (m_towerType) {
			case TowerType.ArrowTower:

			break;
			case TowerType.CannonTower:
				Vector3 targetDir = m_target.transform.position - m_turret.position;
				targetDir.y = 0;
				Quaternion rotation = Quaternion.LookRotation(targetDir);
				m_turret.rotation = rotation;
			break;
			case TowerType.TarCauldron:

			break;			
			default:
				Debug.Log("unknown tower type");
			break;
		}
	}
}
