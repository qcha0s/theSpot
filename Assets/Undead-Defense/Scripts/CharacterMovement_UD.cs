using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement_UD : MonoBehaviour {

	public float m_rotationSpeed = 180;
	public Transform m_imagePos;

	private BaseMovement_UD m_movement;

	void Start () {
		Camera.main.GetComponent<BS_ThirdPersonCamera>().Target = transform;
		m_movement = GetComponent<BaseMovement_UD>();
	}
	
	void Update () {
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
		m_movement.Move(movement.normalized);
		transform.rotation = RotateWithCursor();
		DrawCursor();
	}

	Quaternion RotateWithCursor(){
		Vector3 rotation = transform.rotation.eulerAngles;
		rotation.y += Input.GetAxis("Mouse X") * m_rotationSpeed * Time.deltaTime;
		return Quaternion.Euler(rotation);
	}

	void DrawCursor() {
		Vector2 point = new Vector2(Screen.width*0.5f, Screen.height*0.5f);
		m_imagePos.position = point;
	}
}
