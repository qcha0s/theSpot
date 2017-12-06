using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueControls : MonoBehaviour {
	public Transform m_ultiTarget;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.K)){
			gameObject.transform.position = m_ultiTarget.position;
		}
	}
}
