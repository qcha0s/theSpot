using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager_UD : MonoBehaviour {

	public enum m_gameObjects {ArrowTower,CannonTower,TarTower,EnemyNormal,EnemyFlying,EnemyBoss,Arrow,CannonBall,TarField}
	public PooledObject[] m_objectTypes;
	[Serializable]
	public class PooledObject {
		public GameObject m_objectPrefab;
		public int m_numInPool = 10;
		[HideInInspector]
		public GameObject[] m_objects;
	}
	public static PoolManager_UD Instance { get{ return m_instance; }} 

	private GameObject m_poolContainer;
	private GameObject[] m_objectContainers;
	private static PoolManager_UD m_instance = null;

	private void Awake() {
		if (m_instance == null) {
			m_instance = this;
		}
		m_poolContainer = new GameObject();
		m_poolContainer.name = "pooledObjects";
		InitObjectArrays();
	}

	private void InitObjectArrays() {
		m_objectContainers = new GameObject[m_objectTypes.Length];
		for (int i = 0; i < m_objectTypes.Length; i++) {
			InitContainer(i);
			SpawnObjects(i);
		}
	}

	private void InitContainer(int i) {
		m_objectContainers[i] = new GameObject();
		m_objectContainers[i].transform.parent = m_poolContainer.transform;
		m_objectContainers[i].name = m_objectTypes[i].m_objectPrefab.name;
	}

	private void SpawnObjects(int i) {
		m_objectTypes[i].m_objects = new GameObject[m_objectTypes[i].m_numInPool];
		for (int j = 0; j < m_objectTypes[i].m_numInPool; j++) {
			GameObject temp = Instantiate(m_objectTypes[i].m_objectPrefab);
			temp.SetActive(false);
			temp.transform.parent = m_objectContainers[i].transform;
			m_objectTypes[i].m_objects[j] = temp;
		}
	}

	public void GetArrowTower(Vector3 position) {
		for (int i = 0; i < m_objectTypes[(int)m_gameObjects.ArrowTower].m_numInPool; i++) {
			GameObject tower = m_objectTypes[(int)m_gameObjects.ArrowTower].m_objects[i];
			if (!tower.activeInHierarchy) {
				tower.transform.position = position;
				tower.SetActive(true);
				return;
			}
		}
	}
}
