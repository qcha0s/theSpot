using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Mage : MonoBehaviour {

	public GameObject fireBallPrefab;
	public GameObject polyPrefab;
	public GameObject ultimatePrefab;
	public Transform spellSpawn;
	public Transform ultPos;

	private Animator m_animationController;

	// Use this for initialization
	void Start () {
		m_animationController = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)) {
			m_animationController.SetTrigger("isAttacking");
			Debug.Log("pressed F");
		}
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			m_animationController.SetTrigger("isAbility1");
			Debug.Log("pressed 1");
			m_animationController.SetTrigger("isPoly");
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			m_animationController.SetTrigger("isAbility2");
			Debug.Log("pressed 2");
			m_animationController.SetTrigger("isBlinking");

		}
		if(Input.GetKeyDown(KeyCode.Alpha3)) {
			m_animationController.SetTrigger("isAbility3");
			Debug.Log("pressed 3");
			m_animationController.SetTrigger("isUltimate");
		}
	}

	void Fire() 	{
    // Create the Bullet from the Bullet Prefab
    var bullet = (GameObject)Instantiate (fireBallPrefab, spellSpawn.position, spellSpawn.rotation);

    // Add velocity to the bullet
    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

    // Destroy the bullet after 2 seconds
    Destroy(bullet, 2.0f);
	}

	void Blink() {
		transform.position += transform.rotation * Vector3.forward * 5;
	}

	void Polymorph() 	{
    // Create the Bullet from the Bullet Prefab
    var bullet = (GameObject)Instantiate (polyPrefab, spellSpawn.position, spellSpawn.rotation);

    // Add velocity to the bullet
    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

    // Destroy the bullet after 2 seconds
    Destroy(bullet, 2.0f);
	}

	void Ultimate() 	{
    // Create the Bullet from the Bullet Prefab
    var bullet = (GameObject)Instantiate (ultimatePrefab, ultPos.position, ultPos.rotation);

    // Add velocity to the bullet
    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

    // Destroy the bullet after 2 seconds
    Destroy(bullet, 2.0f);
	}
	
}
