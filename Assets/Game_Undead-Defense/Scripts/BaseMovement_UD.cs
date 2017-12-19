using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement_UD : MonoBehaviour {

	public float m_speed;

	private Rigidbody m_rb;
	
	private void Start() {
		m_rb = GetComponent<Rigidbody>();
	}

	public Vector3 Move(Vector3 direction) {
		if (direction.magnitude > 1) {
			direction = direction.normalized;
		}
		direction *= (Time.deltaTime * m_speed);
		direction = transform.TransformVector(direction);
		Vector3 movement = new Vector3(direction.x,m_rb.velocity.y,direction.z);
		m_rb.velocity = movement;
		return movement;
	}
}
