using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z+3f);
			
		} else if (Input.GetButton ("Fire2")) {
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z-3f);
			
		}	
	}
}
