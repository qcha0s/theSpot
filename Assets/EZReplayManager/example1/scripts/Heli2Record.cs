using UnityEngine;
using System.Collections;

public class Heli2Record : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EZReplayManager.get.mark4Recording(gameObject,"Heli");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
