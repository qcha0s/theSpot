using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement_UD : MonoBehaviour {

	public float m_rotationSpeed = 180;
	public Transform m_imagePos;
	public float m_raycastDistance = 10f;

	private BaseMovement_UD m_movement;
	private Block_UD m_hoveredBlock;

	void Start () {
		Camera.main.GetComponent<BS_ThirdPersonCamera>().Target = transform;
		m_movement = GetComponent<BaseMovement_UD>();
	}
	
	void Update () {
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
		m_movement.Move(movement.normalized);
		RotateWithCursor();
		DrawCursor();
		RayCastForBlock();
	}

	void RotateWithCursor(){
		Vector3 rotation = transform.rotation.eulerAngles;
		rotation.y += Input.GetAxis("Mouse X") * m_rotationSpeed * Time.deltaTime;
		transform.rotation = Quaternion.Euler(rotation);
	}

	void DrawCursor() {
		Vector2 point = new Vector2(Screen.width*0.5f, Screen.height*0.5f);
		m_imagePos.position = point;
	}

	void RayCastForBlock() {
		RaycastHit hit;
		Vector3 startPos = Camera.main.transform.position;
		Vector3 forward = Camera.main.transform.forward * m_raycastDistance;
		Debug.DrawRay(startPos, forward, Color.blue);
		if (Physics.Raycast(startPos,forward, out hit)) {
			Debug.Log(hit.collider.name);
			if (hit.collider.name == "Block") {
				Block_UD newBlock = m_hoveredBlock = hit.collider.GetComponent<Block_UD>();
				if (m_hoveredBlock == null) {
					m_hoveredBlock = newBlock;
				} else if (newBlock != m_hoveredBlock) {
					m_hoveredBlock.NormalizeBlock();
					m_hoveredBlock = newBlock;
				}
				m_hoveredBlock.HighlightBlock();
			} else if (m_hoveredBlock != null) {
			m_hoveredBlock.NormalizeBlock();
			m_hoveredBlock = null;
			}
		} else if (m_hoveredBlock != null) {
			m_hoveredBlock.NormalizeBlock();
			m_hoveredBlock = null;
		}
	}
}
