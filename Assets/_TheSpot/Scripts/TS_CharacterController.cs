using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_CharacterController : MonoBehaviour {

	private float m_speed = 5.0f;
	private float m_sensitivity = 2.0f;

	public CharacterController m_player;
	float m_moveFB;
	float m_moveRL;
	float m_rotationY;
	float m_rotationX;
	float m_maxrotation;
	public GameObject m_eyes;


	// Use this for initialization
	void Start () {
		m_player = GetComponent<CharacterController>();
		
	}

	void Update() {

		Movement();
	}
	
	// Update is called once per frame
	void Movement () {
		m_moveFB = Input.GetAxis("Vertical") * m_speed;
		m_moveRL = Input.GetAxis("Horizontal") * m_speed;

		m_rotationY -= Input.GetAxis("Mouse Y") * m_sensitivity;
		m_rotationX = Input.GetAxis("Mouse X") * m_sensitivity;

		Vector3 movement = new Vector3(m_moveRL, 0, m_moveFB);
		movement = transform.rotation * movement;
		transform.Rotate(0, m_rotationX, 0);
		m_eyes.transform.localRotation = Quaternion.Euler(m_rotationY, 0, 0);

		m_player.Move (movement * Time.deltaTime);



		
	}
}
