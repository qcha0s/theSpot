using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if(!(other.gameObject.name == "Mage")) {
			if(other.gameObject.tag == "Player") {
				Destroy(this.gameObject);
			}
		}
	}
}
