// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class MouseOrbitZoom : MonoBehaviour {
public Transform target;
public float distance= 10.0f;
	
public int MouseWheelSensitivity = 5;
public int MouseZoomMin = 5;
public int MouseZoomMax = 35;	

public float xSpeed= 250.0f;
float ySpeed= 120.0f;

public float yMinLimit;
public float yMaxLimit;

private float x= 0.0f;
private float y= 0.0f;

void  Start (){
    Vector2 angles= transform.eulerAngles;
    x = angles.y;
    y = angles.x;

	// Make the rigid body not change rotation
   	if (GetComponent<Rigidbody>())
		GetComponent<Rigidbody>().freezeRotation = true;
	
	
}

void  Update (){
	
    if (target && Input.GetAxis("Fire2") > 0) {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
		if (y - Input.GetAxis("Mouse Y") * ySpeed * 0.02f > yMinLimit
			&& y - Input.GetAxis("Mouse Y") * ySpeed * 0.02f < yMaxLimit)  
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
 		
        UpdateManually();
    }
	
	if (Input.GetAxis("Mouse ScrollWheel") != 0) {

		if (distance >= MouseZoomMin && distance <= MouseZoomMax){

			distance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;

			if (distance < MouseZoomMin){distance = MouseZoomMin;}
			if (distance > MouseZoomMax){distance = MouseZoomMax;}
			UpdateManually();
		}
	}	
}

public void UpdateManually() {
        Quaternion rotation= Quaternion.Euler(y, x, 0);
		Vector3 temp = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position= rotation * temp + target.position;
        
        transform.rotation = rotation;
        transform.position = position;	
}

static float  ClampAngle ( float angle ,   float min ,   float max  ){
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}
}