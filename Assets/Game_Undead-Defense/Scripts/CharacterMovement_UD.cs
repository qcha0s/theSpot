using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement_UD : MonoBehaviour {

	public float m_rotationSpeed = 180;
	public float m_raycastDistance = 10f;

	private BaseMovement_UD m_movement;
	private Animator m_anim;

	private SphereCollider m_DamageBox;
	private bool m_readyToFire = true;
	private float m_fireRate = 1.5f;
	public Rigidbody m_arrow; 
	public Transform m_firePoint;
	public bool isFiring = true;
	public float arrowSpeed;
	private float fireSpeed = 1;
	private float fireRate = 2;

	private void Awake() {
		m_anim = GetComponent<Animator>();
	}

	void Start () {
		Camera.main.GetComponent<BS_ThirdPersonCamera>().Target = transform;
		m_movement = GetComponent<BaseMovement_UD>();
		m_DamageBox = GetComponentInChildren<SphereCollider>();
		isFiring = false;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
			if(fireRate <= 0) {
				isFiring = true;
				m_anim.SetTrigger("isReady");
				m_anim.SetTrigger("isAim");
				m_anim.SetTrigger("isShoot");
				fireRate = 2;
			}
		}
		fireRate -= Time.deltaTime * fireSpeed;
	}
	
	void FixedUpdate () {
		if (isFiring == false) {
			Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
			Vector3 speed = m_movement.Move(movement.normalized);
			speed = transform.InverseTransformVector(speed);
			m_anim.SetFloat("forward", speed.z);
			m_anim.SetFloat("right", speed.x);
			RotateWithCursor();
		}
	}

	void RotateWithCursor(){
		Vector3 rotation = transform.rotation.eulerAngles;
		rotation.y += Input.GetAxis("Mouse X") * m_rotationSpeed * Time.deltaTime;
		transform.rotation = Quaternion.Euler(rotation);
	}

	void ShootArrow () {
		Rigidbody arrow = Instantiate(m_arrow, m_firePoint.position, Quaternion.identity) as Rigidbody;
		arrow.AddForce(m_firePoint.forward * arrowSpeed, ForceMode.Impulse);
	}

	void Moving () {
		isFiring = false;
	}

	void NotMoving () {
		isFiring = true;
	}
}
