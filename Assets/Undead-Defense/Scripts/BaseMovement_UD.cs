using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement_UD : MonoBehaviour {

	public float m_speed;
	
	public void Move(Vector3 direction) {
		direction *= (Time.deltaTime * m_speed);
		transform.Translate(direction.x,0,direction.z);
	}
}
