using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepCollider : MonoBehaviour {

	public PolyManager m_polyScript;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.name != this.gameObject.name) {
			if(!(other.gameObject.name == "Polymorph(Clone)")) {
				m_polyScript.CancelPoly();
			}
		}
	}
}
