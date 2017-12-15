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
	public int m_enemyClusterAmount = 10;
	public int m_enemySpawnRate = 1;

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
		WaveManager.instance.Spawner = this;
	}

	public int SpawnEnemies(int waveNumber = 1) {
		int numEnemiesSpawned = 0;
		numEnemiesSpawned += SpawnSkeletons(waveNumber);
		numEnemiesSpawned += SpawnFlyingUnits(waveNumber);
		numEnemiesSpawned += SpawnBoss(waveNumber);
		return numEnemiesSpawned;
	}

	public int SpawnSkeletons(int waveNumber) {
		int numToSpawn = waveNumber * 20;
		int numSpawned = 0;
		for (int i = 0; i < m_WPContainers.Length; i++) {
			int spawnAmount = 0;
			if (i < m_WPContainers.Length-1) {
				spawnAmount = numToSpawn/m_WPContainers.Length;
				numSpawned += spawnAmount;
				StartCoroutine(SpawnSkele(spawnAmount,m_WPContainers[i]));
			} else {
				spawnAmount = numToSpawn - numSpawned;
				StartCoroutine(SpawnSkele(spawnAmount,m_WPContainers[i]));
			}
		}
		return numToSpawn;
	}

	public int SpawnFlyingUnits(int waveNumber) {
		int numToSpawn = 0;
		if (m_hasFlyingUnits) {
			numToSpawn = ((waveNumber/3) + (waveNumber/3 * waveNumber % 3)) * 5;
			int numSpawned = 0;
			for (int i = 0; i < m_WPContainers.Length; i++) {
				int spawnAmount = 0;
				if (i < m_WPContainers.Length-1) {
					spawnAmount = numToSpawn/m_WPContainers.Length;
					numSpawned += spawnAmount;
					StartCoroutine(SpawnFlyer(spawnAmount,m_WPContainers[i]));
				} else {
					spawnAmount = numToSpawn - numSpawned;
					StartCoroutine(SpawnFlyer(spawnAmount,m_WPContainers[i]));
				}
			}
		}
		return numToSpawn;
	}

	public int SpawnBoss(int waveNumber) {
		int numToSpawn = 0;
		if (m_hasBossUnits) {
			numToSpawn = waveNumber/5;
			int numSpawned = 0;
			for (int i = 0; i < m_WPContainers.Length; i++) {
				int spawnAmount = 0;
				if (i < m_WPContainers.Length-1) {
					spawnAmount = numToSpawn/m_WPContainers.Length;
					numSpawned += spawnAmount;
					StartCoroutine(SpawnBossMan(spawnAmount,m_WPContainers[i]));
				} else {
					spawnAmount = numToSpawn - numSpawned;
					StartCoroutine(SpawnBossMan(spawnAmount,m_WPContainers[i]));
				}
			}
		}
		return numToSpawn;
	}

	IEnumerator SpawnSkele(int numToSpawn, WPContainer enemyWP) {
		int numSpawned = 0;
		while (numSpawned < numToSpawn) {
			for (int i = 0; i < m_enemyClusterAmount; i++) {
				GameObject enemy = PoolManager_UD.Instance.GetObject((int)UD_Objects.EnemyNormal);
				enemy.SetActive(true);
				enemy.transform.position = enemyWP.StartPoint.position;
				enemy.GetComponent<NavMeshAgent>().Warp(enemyWP.StartPoint.position);
				enemy.GetComponent<NavWaypointAI_UD>().SetWaypoints(enemyWP.Waypoints);
				numSpawned+=1;
				if (numSpawned == numToSpawn) {
					break;
				}
			}
			yield return new WaitForSeconds(m_enemySpawnRate);
		}
	}

	IEnumerator SpawnFlyer(int numToSpawn, WPContainer enemyWP) {
		int numSpawned = 0;
		while (numSpawned < numToSpawn) {
			for (int i = 0; i < m_enemyClusterAmount; i++) {
				GameObject enemy = PoolManager_UD.Instance.GetObject((int)UD_Objects.EnemyFlying);
				enemy.SetActive(true);
				Vector3 EnemyPos = enemyWP.StartPoint.position;
				EnemyPos.y += 3;
				enemy.transform.position = EnemyPos;
				enemy.GetComponent<NavMeshAgent>().Warp(EnemyPos);
				enemy.GetComponent<NavWaypointAI_UD>().SetWaypoints(enemyWP.Waypoints);
				numSpawned+=1;
				if (numSpawned == numToSpawn) {
					break;
				}
			}
			yield return new WaitForSeconds(m_enemySpawnRate);
		}
	}

	IEnumerator SpawnBossMan(int numToSpawn, WPContainer enemyWP) {
		int numSpawned = 0;
		while (numSpawned < numToSpawn) {
			for (int i = 0; i < m_enemyClusterAmount; i++) {
				GameObject enemy = PoolManager_UD.Instance.GetObject((int)UD_Objects.EnemyBoss);
				enemy.SetActive(true);
				enemy.transform.position = enemyWP.StartPoint.position;
				enemy.GetComponent<NavMeshAgent>().Warp(enemyWP.StartPoint.position);
				enemy.GetComponent<NavWaypointAI_UD>().SetWaypoints(enemyWP.Waypoints);
				numSpawned+=1;
				if (numSpawned == numToSpawn) {
					break;
				}
			}
			yield return new WaitForSeconds(m_enemySpawnRate);
		}
	}
}
