using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement_UD : MonoBehaviour {

	public float m_speed;

	private Rigidbody m_rb;
	
	private void Start() {
		m_rb = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 direction) {
		if (direction.magnitude > 1) {
			direction = direction.normalized;
		}
		direction *= (Time.deltaTime * m_speed);
		direction = transform.TransformVector(direction);
		m_rb.velocity = new Vector3 (direction.x,m_rb.velocity.y,direction.z);
	}
}
