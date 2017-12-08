<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_ThirdPersonCamera : MonoBehaviour {
	#region Public Variables
	public float m_offsetFromWall = 0.1f;
	public float m_maxLookDownAngle = 60.0f;
	public float m_defaultLookDownAngle = 15.0f;
	public float m_rotationSpeed = 30.0f;
	public float m_minZoom = 0.6f;
	public float m_maxZoom = 20f;
	public float m_defaultZoom = 5.0f;	
	public float m_zoomSpeed = 5.0f;
	public Vector3 m_pivotOffset = new Vector3(0.5f, 1.5f, 0);
	public LayerMask m_collisionMask;
	#endregion
	#region Private Variables
	private float m_lastUncollidedZoom;
	private float m_zoom;
	private float m_lookDownAngle;
	private Transform m_target;
	#endregion
	public Transform Target{
		get{
			return m_target;
		}
		set{
			m_target = value;
		}
	}
	// Use this for initialization
	void Start () {
		// SetCursorState(CursorLockMode.Locked);
		m_zoom = m_defaultZoom;
		m_lastUncollidedZoom = m_defaultZoom;
		m_lookDownAngle = m_defaultLookDownAngle;
	}
	void Update(){
		
	}
	void LateUpdate(){
		m_zoom -= Input.GetAxis("Mouse ScrollWheel") * m_zoomSpeed * Time.deltaTime * Mathf.Abs(m_zoom);
		m_lookDownAngle -= Input.GetAxis("Mouse Y") * m_rotationSpeed * Time.deltaTime;
		m_zoom = Mathf.Clamp(m_zoom, m_minZoom, m_maxZoom);
		m_lookDownAngle = Mathf.Clamp(m_lookDownAngle, -m_maxLookDownAngle, m_maxLookDownAngle);
		if(m_target != null){
			Quaternion rotation = CalculateCameraRotation(m_target);

			Vector3 pivot = m_target.position + m_target.rotation * m_pivotOffset;
			Vector3 position = CalculateCameraPosition(rotation, pivot);
			transform.rotation = rotation;
			transform.position = position;
		}
		
	}
	Quaternion CalculateCameraRotation(Transform target){
		Vector3 eulerRotation = target.rotation.eulerAngles;
		eulerRotation.x = m_lookDownAngle;
		return Quaternion.Euler(eulerRotation);
	}
	Vector3 CalculateCameraPosition(Quaternion rotation, Vector3 pivot){
		Vector3 rotatedBack = rotation * -Vector3.forward;
		RaycastHit collision;
		Vector3 desiredPosition = pivot + rotatedBack * m_zoom;
		
		if(Physics.Linecast(pivot, desiredPosition, out collision, m_collisionMask)){
			m_zoom = Vector3.Distance(pivot, collision.point) - m_offsetFromWall;
		}else{
			// m_lastUncollidedZoom = m_zoom;
		}
		
		m_zoom = Mathf.Clamp(m_zoom, m_minZoom, m_maxZoom);
		Vector3 calculatedPosition = pivot + rotatedBack * m_zoom;
		return calculatedPosition;
	}
}
=======



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_ThirdPersonCamera : MonoBehaviour {
	#region Public Variables
	public float m_offsetFromWall = 0.1f;
	public float m_maxLookDownAngle = 60.0f;
	public float m_defaultLookDownAngle = 15.0f;
	public float m_rotationSpeed = 30.0f;
	public float m_minZoom = 0.6f;
	public float m_maxZoom = 20f;
	public float m_defaultZoom = 5.0f;	
	public float m_zoomSpeed = 5.0f;
	public Vector3 m_pivotOffset = new Vector3(0.5f, 1.5f, 0);
	public LayerMask m_collisionMask;
	#endregion
	#region Private Variables
	private float m_lastUncollidedZoom;
	private float m_zoom;
	private float m_lookDownAngle;
	private Transform m_target;
	#endregion
	public Transform Target{
		get{
			return m_target;
		}
		set{
			m_target = value;
		}
	}
	// Use this for initialization
	void Start () {
		// SetCursorState(CursorLockMode.Locked);
		m_zoom = m_defaultZoom;
		m_lastUncollidedZoom = m_defaultZoom;
		m_lookDownAngle = m_defaultLookDownAngle;
	}
	void Update(){
		
	}
	void LateUpdate(){
		m_zoom -= Input.GetAxis("Mouse ScrollWheel") * m_zoomSpeed * Time.deltaTime * Mathf.Abs(m_zoom);
		m_lookDownAngle -= Input.GetAxis("Mouse Y") * m_rotationSpeed * Time.deltaTime;
		m_zoom = Mathf.Clamp(m_zoom, m_minZoom, m_maxZoom);
		m_lookDownAngle = Mathf.Clamp(m_lookDownAngle, -m_maxLookDownAngle, m_maxLookDownAngle);
		if(m_target != null){
			Quaternion rotation = CalculateCameraRotation(m_target);

			Vector3 pivot = m_target.position + m_target.rotation * m_pivotOffset;
			Vector3 position = CalculateCameraPosition(rotation, pivot);
			transform.rotation = rotation;
			transform.position = position;
		}
		
	}
	Quaternion CalculateCameraRotation(Transform target){
		Vector3 eulerRotation = target.rotation.eulerAngles;
		eulerRotation.x = m_lookDownAngle;
		return Quaternion.Euler(eulerRotation);
	}
	Vector3 CalculateCameraPosition(Quaternion rotation, Vector3 pivot){
		Vector3 rotatedBack = rotation * -Vector3.forward;
		RaycastHit collision;
		Vector3 desiredPosition = pivot + rotatedBack * m_zoom;
		
		if(Physics.Linecast(pivot, desiredPosition, out collision, m_collisionMask)){
			m_zoom = Vector3.Distance(pivot, collision.point) - m_offsetFromWall;
		}else{
			// m_lastUncollidedZoom = m_zoom;
		}
		
		m_zoom = Mathf.Clamp(m_zoom, m_minZoom, m_maxZoom);
		Vector3 calculatedPosition = pivot + rotatedBack * m_zoom;
		return calculatedPosition;
	}
}
>>>>>>> TheSpot
