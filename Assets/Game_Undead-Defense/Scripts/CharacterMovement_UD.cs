using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement_UD : MonoBehaviour {

	public float m_rotationSpeed = 180;
	public float m_raycastDistance = 10f;

	private BaseMovement_UD m_movement;
	private Animator m_anim;

	private void Awake() {
		m_anim = GetComponent<Animator>();
	}

	void Start () {
		Camera.main.GetComponent<BS_ThirdPersonCamera>().Target = transform;
		m_movement = GetComponent<BaseMovement_UD>();
	}
	
	void Update () {
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
		Vector3 speed = m_movement.Move(movement.normalized);
		speed = transform.InverseTransformVector(speed);
		m_anim.SetFloat("forward", speed.z);
		m_anim.SetFloat("right", speed.x);
		RotateWithCursor();
	}

	void RotateWithCursor(){
		Vector3 rotation = transform.rotation.eulerAngles;
		rotation.y += Input.GetAxis("Mouse X") * m_rotationSpeed * Time.deltaTime;
		transform.rotation = Quaternion.Euler(rotation);
	}
}
