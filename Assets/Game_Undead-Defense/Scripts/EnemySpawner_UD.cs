using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemySpawner_UD : MonoBehaviour {

	public WPContainer[] m_WPContainers;
	public static EnemySpawner_UD Instance { get{ return m_instance;}}
	public bool m_hasFlyingUnits = false;
	public bool m_hasBossUnits = false;

	private static EnemySpawner_UD m_instance = null;

	[Serializable]
	public class WPContainer {
		public Transform[] Waypoints { get{ return m_waypoints; }}
		public Transform StartPoint { get{ return m_startPoint; }}
		public Transform m_waypointsParent;
		[HideInInspector]
		private Transform[] m_waypoints;
		private Transform m_startPoint;
		public void GetWayPoints() {
			Transform[] potentialWaypoints = m_waypointsParent.GetComponentsInChildren<Transform>();
			m_waypoints = new Transform[ (potentialWaypoints.Length - 2) ];
			m_startPoint = potentialWaypoints[1];
			for (int i = 2; i < potentialWaypoints.Length; i++ ) {
				m_waypoints[i - 2] = potentialWaypoints[i];
			}			
		}
	}

	private void Start() {
		m_instance = this;
		for (int i = 0; i < m_WPContainers.Length; i++) {
			m_WPContainers[i].GetWayPoints();
		}
		SpawnEnemies(1);
	}

	public int SpawnEnemies(int waveNumber = 1) {
		int numEnemiesSpawned = 0;
		numEnemiesSpawned += SpawnSkeletons(waveNumber);
		numEnemiesSpawned += SpawnFlyingUnits(waveNumber);
		numEnemiesSpawned += SpawnBoss(waveNumber);
		return numEnemiesSpawned;
	}

	public int SpawnSkeletons(int waveNumber) {
		int numToSpawn = waveNumber * 10;
		for (int i = 0; i < numToSpawn; i++) {
			GameObject enemy = PoolManager_UD.Instance.GetObject((int)UD_Objects.EnemyNormal);
			WPContainer enemyWPs = m_WPContainers[UnityEngine.Random.Range(0,m_WPContainers.Length-1)];
			enemy.SetActive(true);
			enemy.transform.position = enemyWPs.StartPoint.position;
			enemy.GetComponent<NavMeshAgent>().Warp(enemyWPs.StartPoint.position);
			enemy.GetComponent<NavWaypointAI_UD>().SetWaypoints(enemyWPs.Waypoints);
		}
		return numToSpawn;
	}

	public int SpawnFlyingUnits(int waveNumber) {
		int numToSpawn = 0;
		if (m_hasFlyingUnits) {
			numToSpawn = ((waveNumber/3) + (waveNumber/3 * waveNumber % 3)) * 5;
			for (int i = 0; i < numToSpawn; i++) {
				GameObject enemy = PoolManager_UD.Instance.GetObject((int)UD_Objects.EnemyFlying);
				WPContainer enemyWPs = m_WPContainers[UnityEngine.Random.Range(0,m_WPContainers.Length-1)];
				enemy.SetActive(true);
				enemy.transform.position = enemyWPs.StartPoint.position;
				enemy.GetComponent<NavMeshAgent>().Warp(enemyWPs.StartPoint.position);
				enemy.GetComponent<NavWaypointAI_UD>().SetWaypoints(enemyWPs.Waypoints);
			}
		}
		return numToSpawn;
	}

	public int SpawnBoss(int waveNumber) {
		int numToSpawn = 0;
		if (m_hasBossUnits) {
			numToSpawn = waveNumber/5;
			for (int i = 0; i < numToSpawn; i++) {
				GameObject enemy = PoolManager_UD.Instance.GetObject((int)UD_Objects.EnemyBoss);
				WPContainer enemyWPs = m_WPContainers[UnityEngine.Random.Range(0,m_WPContainers.Length-1)];
				enemy.GetComponent<NavWaypointAI_UD>().SetWaypoints(enemyWPs.Waypoints);
				enemy.SetActive(true);
				enemy.transform.position = enemyWPs.StartPoint.position;
				enemy.GetComponent<NavMeshAgent>().Warp(enemyWPs.StartPoint.position);
				enemy.GetComponent<NavWaypointAI_UD>().SetWaypoints(enemyWPs.Waypoints);
			}
		}
		return numToSpawn;
	}
}
