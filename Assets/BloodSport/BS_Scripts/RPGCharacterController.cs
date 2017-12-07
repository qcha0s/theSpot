using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCharacterController : MonoBehaviour {
	public string m_moveStatus = "idle";
	public bool m_walkByDefault = true;
	public float m_gravity = 20.0f;

	public Collider[] m_weaponHitBoxes;

	//Movement Speeds
	public float m_jumpSpeed = 8.0f;
	public float m_runSpeed = 10.0f;
	public float m_walkSpeed = 4.0f;
	public float m_turnSpeed = 250.0f;
	public float m_moveBackwardsMultiplier = 0.75f;

	//Internal Variables
	private float m_speedMultiplier = 0.0f;
	private bool m_grounded = false;
	private Vector3 m_moveDirection = Vector3.zero;
	private bool m_isWalking = false;
	private bool m_jumping = false;	
	private Animator m_animationController;
	private bool m_mouseSideDown;
	private CharacterController m_controller;
	private int m_attackState;



	void Awake(){

		m_controller = GetComponent<CharacterController>();
		m_animationController = GetComponent<Animator>();
		
	}

	void Update(){
		m_moveStatus = "idle";
		m_isWalking = m_walkByDefault;

		if(Input.GetAxis("Run") != 0){
			m_isWalking = !m_walkByDefault;
		}

		if(m_grounded){
			//if player is steering with the right mouse button .. A/D will strafe
			if(Input.GetMouseButton(1)){
				m_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			}else{
				m_moveDirection = new Vector3(0,0,Input.GetAxis("Vertical"));
			}

			//Automove button
			if(Input.GetButtonDown("Toggle Move")){
				m_mouseSideDown = !m_mouseSideDown;
			}

			if(m_mouseSideDown && (Input.GetAxis("Vertical") != 0 || Input.GetButton("Jump")) || (Input.GetMouseButton(0) && Input.GetMouseButton(1))){
				m_mouseSideDown = false;
			}

			if((Input.GetMouseButton(0) && Input.GetMouseButton(1)) || m_mouseSideDown){
				m_moveDirection.z = 1;
			}
			if(!(Input.GetMouseButton(1) && Input.GetAxis("Horizontal") != 0)){
				m_moveDirection.x -= Input.GetAxis("Strafing");
			}

			if(((Input.GetMouseButton(1) && Input.GetAxis("Horizontal")!=0) || Input.GetAxis("Strafing")!=0) && Input.GetAxis("Vertical") !=0){
				m_moveDirection *= 0.707f;
			}

			if((Input.GetMouseButton(1) && Input.GetAxis("Horizontal")!= 0) || Input.GetAxis("Strafing") !=0 || Input.GetAxis("Vertical") < 0){

				m_speedMultiplier = m_moveBackwardsMultiplier;
			}else{
				m_speedMultiplier = 1f;
			}

			m_moveDirection *= m_isWalking ? m_walkSpeed * m_speedMultiplier : m_runSpeed * m_speedMultiplier;

			if(Input.GetButton("Jump")){
				m_jumping = true;
				m_moveDirection.y = m_jumpSpeed;
			}

			if(m_moveDirection.magnitude > 0.05f){
				m_animationController.SetBool("isRunning",true);
			}else{
				m_animationController.SetBool("isRunning",false);
			}
			m_animationController.SetFloat("Speed", m_moveDirection.z);
			m_animationController.SetFloat("Direction", m_moveDirection.x);

			m_moveDirection = transform.TransformDirection(m_moveDirection);
		}

		if(Input.GetMouseButton(1)){
			transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y,0);
		} else{
			transform.Rotate(0,Input.GetAxis("Horizontal") * m_turnSpeed * Time.deltaTime, 0);
		}
		m_moveDirection.y -= m_gravity * Time.deltaTime;

		m_grounded = ((m_controller.Move(m_moveDirection * Time.deltaTime)) & CollisionFlags.Below) !=0;

		//reset jumping after grounded\
		m_jumping = m_grounded ? false : m_jumping;

		if(m_jumping){
			m_moveStatus = "jump";
			m_animationController.SetBool("isJumping",true);
		}
		else{
			m_animationController.SetBool("isJumping",false);
		}


	
		//is the player attacking
		AnimatorStateInfo currentupperTorsoState = m_animationController.GetCurrentAnimatorStateInfo(1);
		
		if(currentupperTorsoState.fullPathHash == m_attackState){
			// for(int i = 0;i < m_weaponHitBoxes.Length; i++){
			// 	m_weaponHitBoxes[i].enabled = true;
			// }
			Debug.Log(currentupperTorsoState.fullPathHash);
		}
		if(Input.GetMouseButtonDown(0)){
			m_animationController.SetBool("isAttacking",true);
			//m_weaponHitBox.enabled = true;
		}
		else if(Input.GetMouseButtonUp(0)){
			m_animationController.SetBool("isAttacking",false);
			//m_weaponHitBox.enabled = false;
		}
		

	}
}
