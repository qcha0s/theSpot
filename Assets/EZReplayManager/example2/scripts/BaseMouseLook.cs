using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the ScreenToWorldPoint function of the camera

public class BaseMouseLook : MonoBehaviour {


	float posX = 0F;
	float posY = 0F;
	
//	Quaternion originalRotation;

	void Update ()
	{
		Player p = (Player)transform.parent.parent.GetComponent<Player>();
		//Monitor gameMonitor = GameObject.Find("Game").GetComponent<Monitor>();
		if (p.isLocalHuman && p.isAlive /*&& gameMonitor.isActive()*/ ) {

			posX = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y,100)).x;
			posY = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y,100)).y;			
			transform.LookAt(new Vector3(posX,posY,0f)); 
		}
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
//		originalRotation = transform.localRotation;
	}
}