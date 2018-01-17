using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Bullet : MonoBehaviour {
	
	private BS_testmove testmove; 
	private bool hasCollided = false;
	public BS_Ultimate ultimate;
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * 2;
	}
    void OnTriggerEnter(Collider other){
		ultimate.Charge(1);
		Debug.Log("hit");
		Destroy(gameObject);
	}
}