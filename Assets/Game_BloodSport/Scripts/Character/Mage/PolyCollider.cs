using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyCollider : MonoBehaviour {

	public PolyManager m_polyScript;
	public bool m_isMe = false;
	
	void OnTriggerEnter(Collider other) {
		if(!m_isMe) {
			if(other.gameObject.name == "Polymorph(Clone)") {
				m_polyScript.SetPoly();
			}
		}
	}
}
