using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum UD_Objects {ArrowTower,CannonTower,TarTower,EnemyNormal,EnemyFlying,EnemyBoss,Arrow,CannonBall,TarField}

public class PoolManager_UD : MonoBehaviour {

	public PooledObject[] m_objectTypes;
	[Serializable]
	public class PooledObject {
		public GameObject m_objectPrefab;
		public int m_numInPool = 10;
		[HideInInspector]
		public List<GameObject> m_objects = new List<GameObject>();
	}
	public static PoolManager_UD Instance { get{ return m_instance; }} 

	private GameObject m_poolContainer;
	private GameObject[] m_objectContainers;
	private static PoolManager_UD m_instance = null;
	private const float LISTGROWTHPERCENTAGE = 1.15f;

	private void Awake() {
		//if (m_instance == null) {
		m_instance = this;
		//}
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
		for (int j = 0; j < m_objectTypes[i].m_numInPool; j++) {
			GameObject temp = Instantiate(m_objectTypes[i].m_objectPrefab);
			temp.SetActive(false);
			temp.transform.parent = m_objectContainers[i].transform;
			m_objectTypes[i].m_objects.Add(temp);
		}
	}

	public GameObject GetObject(int type) {
		GameObject obj = null;
		obj = GetObjectFromPool(type);
		if (obj == null) {
			obj = ExpandPool(type);
		}
		return obj;
	}

	// returns a inactive item from the list
	private GameObject GetObjectFromPool(int type) {
		GameObject pooledObj = null;
		for (int i = 0; i < m_objectTypes[type].m_numInPool; i++) {
			GameObject newObj = m_objectTypes[type].m_objects[i];
			if (!newObj.activeInHierarchy) {
				pooledObj = newObj;
				break;
			}
		}
		return pooledObj;
	}

	// expands the list and returns an inactive item from the expanded list
	private GameObject ExpandPool(int type) {
		int currentPoolCapacity = m_objectTypes[type].m_numInPool;
		int newPoolCapacity = (int)(currentPoolCapacity * LISTGROWTHPERCENTAGE);
		int difference = newPoolCapacity - currentPoolCapacity;
		for (int i = 0; i < difference; i++) {
			GameObject temp = Instantiate(m_objectTypes[type].m_objectPrefab);
			temp.SetActive(false);
			temp.transform.parent = m_objectContainers[type].transform;			
			m_objectTypes[type].m_objects.Add(temp);
		}
		GameObject newPoolObj = m_objectTypes[type].m_objects[currentPoolCapacity];
		m_objectTypes[type].m_numInPool = newPoolCapacity;
		return newPoolObj;
	}
}
