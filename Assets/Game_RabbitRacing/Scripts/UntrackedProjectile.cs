using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntrackedProjectile : MonoBehaviour, Item {
	public float m_travelSpeed = 30.0f;
	public void Use(GameObject user){
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = transform.forward * m_travelSpeed;
	}
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
