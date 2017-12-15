using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour {

	public GameObject m_item;                // The item prefab to be spawned.
    public float m_spawnTime = 4f;            // How long between each spawn.
    private float m_spawnTimer = 0;
    public Transform m_spawnPrefab;

    void Start ()
    {
        Spawn();
    }

    void Update () 
    {
        if (m_item == null) {
            m_spawnTimer += Time.deltaTime;
            if (m_spawnTimer >= m_spawnTime) {
                Spawn();
                m_spawnTimer = 0;
            }
        }
    }

    void Spawn ()
    {
        m_item = Instantiate (m_spawnPrefab, transform.position, transform.rotation).gameObject;
        //Instantiate (m_item, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        
    }
}
