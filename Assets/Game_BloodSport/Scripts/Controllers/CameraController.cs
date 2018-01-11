using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform m_target;

	public float m_heightOffset = 1.7f;

	public float m_distance = 12.0f;

	public float m_offsetFromWall = 0.1f;

	public float  m_minDistance = 0.6f;

	public float  m_maxDistance = 20.0f;

	public float m_xSpeed = 200.0f;

	public float m_ySpeed = 200.0f;

	public float m_yMinLimit = -80f;

	public float m_yMaxLimit = 80f;

	public float m_zoomSpeed = 5.0f;

	public float m_autoRotationSpeed = 3.0f;

	public LayerMask m_collisionLayers = -1;

	public bool m_alwaysRotatetoRearofTarget = false;

	public bool m_allowMouseInputX = true;

	public bool m_allowMouseInputY = true;

	private float m_xDeg = 0.0f;

	private float m_yDeg = 0.0f;

	
	private float m_currentDistance;

	private float m_desiredDistance;

	private float m_correctedDistance;

	private bool m_rotateBehind = false;

	private bool m_mouseSideButton = false;
		void Start(){
		 Vector3 angles = transform.eulerAngles;
		m_xDeg = angles.x;
		m_yDeg = angles.y;
		Vector3 distance = m_target.position - transform.position;
		
		m_currentDistance = distance.magnitude;
		m_desiredDistance = m_currentDistance;
		m_correctedDistance = m_currentDistance;

		if(m_alwaysRotatetoRearofTarget){
			m_rotateBehind = true;
		}
	}		

	void LateUpdate(){
		
		if (Input.GetMouseButton(1) && !m_target.GetComponent<RPGCharacterController>().m_disableMovement)
        {
        	m_xDeg += Input.GetAxis("Mouse X") * m_xSpeed * 0.02f;
        	m_yDeg -= Input.GetAxis("Mouse Y") * m_ySpeed * 0.02f;
        	float test = 0;
       		test = m_yDeg;
        }
		else{
			RotateBehindTarget();
		}
		m_distance += -(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * m_zoomSpeed * Mathf.Abs(m_distance);
		if (m_distance < 2.5f)
        {
            m_distance = 2.5f;
        }
        if (m_distance > 20f)
        {
            m_distance = 20f;
        }
		
		m_yDeg = ClampAngle(m_yDeg, m_yMinLimit, m_yMaxLimit);
		// transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)), Vector3.up);
		//automove button pressed
	
		Quaternion rotation = Quaternion.Euler(m_yDeg,m_xDeg,0);
		Vector3 position = rotation * new Vector3(0.0f, 2.0f, -m_distance) + m_target.position;

		transform.rotation = rotation;
		transform.position = position;
	}
	private void RotateBehindTarget(){
		float targetRotationAngle = m_target.transform.eulerAngles.y;
		float currentRotationAngle = transform.eulerAngles.y;
		m_xDeg = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, m_autoRotationSpeed * Time.deltaTime);

		if(targetRotationAngle == currentRotationAngle){
			if(!m_alwaysRotatetoRearofTarget){
				m_rotateBehind = false;
			}
		} else{
			m_rotateBehind = true;
		}
	}

	private float ClampAngle(float angle, float min, float max){
		
		if(angle < -360f){
			angle += 360f;
		}

		if(angle > 360f){
			angle -= 360f;
		}
		
		return Mathf.Clamp(angle, min, max);

	}
}
