using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReady : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameManager_UD.instance.MapReady();
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
