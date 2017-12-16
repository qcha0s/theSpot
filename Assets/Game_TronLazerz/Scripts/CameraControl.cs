using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	
	public Transform currentMount;
	public float speedfactor = 0.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, currentMount.position,speedfactor);
		transform.rotation = Quaternion.Slerp(transform.rotation,currentMount.rotation,speedfactor);
	}
	public void setMount(Transform newMount){
		currentMount = newMount;


	}
}
