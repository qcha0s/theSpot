using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Mage : MonoBehaviour {

	public GameObject fireBallPrefab;
	public Transform fireBallSpawn;

	private Animator m_animationController;

	// Use this for initialization
	void Start () {
		m_animationController = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)) {
			m_animationController.SetTrigger("isAttacking");
		}
	}

	void Fire() 	{
    // Create the Bullet from the Bullet Prefab
    var bullet = (GameObject)Instantiate (fireBallPrefab, fireBallSpawn.position, fireBallSpawn.rotation);

    // Add velocity to the bullet
    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

    // Destroy the bullet after 2 seconds
    Destroy(bullet, 2.0f);
	}
}
