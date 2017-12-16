using UnityEngine;
using System.Collections;

public class HeliMovement : MonoBehaviour {

	public Transform topHelix;
	public float speed;
	private float toleranceY = 10f;
	// Use this for initialization
	void Start () {
		speed += Random.Range(speed-0.03f,speed+0.03f);
		toleranceY +=  Random.Range(toleranceY-3f,toleranceY+3f);
	}
	
	void OnMouseDown() {
		
	}
	
	void OnMouseUp() {
		
	}
		
	void FixedUpdate () {

		if (Input.mousePosition.x > 0 && Input.mousePosition.x < Screen.width && 
			Input.mousePosition.y > 0 && Input.mousePosition.y < Screen.height) {
			float wantedPosY = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y,100)).y;
			
			
			float movementY = speed;
			if (transform.position.y < wantedPosY - toleranceY) {
				//movementY = 1f;
			} else if (transform.position.y > wantedPosY + toleranceY)
				movementY *= -1;
			transform.position = new Vector3(transform.position.x,transform.position.y+movementY,transform.position.z);
		}
		
		transform.position = new Vector3(transform.position.x+speed,transform.position.y,transform.position.z);
		float helixSpeed = 200f * speed;
		topHelix.localEulerAngles= new Vector3(topHelix.localEulerAngles.x,topHelix.localEulerAngles.y,topHelix.localEulerAngles.z+helixSpeed);
		//backHelix.localEulerAngles = new Vector3(backHelix.localEulerAngles.x+helixSpeed,backHelix.localEulerAngles.y,backHelix.localEulerAngles.z);´		
	}
}
