using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter(Collision other) {
		if(!(other.gameObject.name == "Mage")) {
			Destroy(this.gameObject);
		}
	}
}
