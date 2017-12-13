using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	private void Start() {
		EZReplayManager.get.mark4Recording(gameObject,"Projectile");
	}	
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision) {
		Transform dummy = collision.transform.Find("dummy");
		if (dummy != null) {
	
			if (dummy.gameObject.GetComponent<DummyAI>()) {
				dummy.gameObject.GetComponent<DummyAI>().hit(collision.relativeVelocity.magnitude);
			} 
		}			
	}
}
