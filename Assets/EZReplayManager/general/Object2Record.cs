using UnityEngine;
using System.Collections;

public class Object2Record : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EZReplayManager.get.mark4Recording(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
