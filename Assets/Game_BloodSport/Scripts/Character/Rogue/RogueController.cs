using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Assets/Game_BloodSport/Scripts/Character/Rogue/RogueController.cs
public class RogueController : MonoBehaviour {
=======
public class RPGCharacterController : MonoBehaviour {
>>>>>>> undead-defense:Assets/BloodSport/BS_Scripts/RPGCharacterController.cs
	public string m_moveStatus = "idle";
	public bool m_walkByDefault = true;
	public float m_gravity = 20.0f;

	public Collider[] m_weaponHitBoxes;
<<<<<<< HEAD:Assets/Game_BloodSport/Scripts/Character/Rogue/RogueController.cs
	public GameObject m_targetGUI;
	public GameObject[] m_weapons;
=======
>>>>>>> undead-defense:Assets/BloodSport/BS_Scripts/RPGCharacterController.cs

	//Movement Speeds
	public float m_jumpSpeed = 8.0f;
	public float m_runSpeed = 10.0f;
	public float m_walkSpeed = 4.0f;
	public float m_turnSpeed = 250.0f;
	public float m_moveBackwardsMultiplier = 0.75f;
<<<<<<< HEAD:Assets/Game_BloodSport/Scripts/Character/Rogue/RogueController.cs
	public bool m_hasDealtDamage = false;
	public bool m_disableMovement = false;
=======
>>>>>>> undead-defense:Assets/BloodSport/BS_Scripts/RPGCharacterController.cs

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
<<<<<<< HEAD:Assets/Game_BloodSport/Scripts/Character/Rogue/RogueController.cs
	private bool m_UltActive = true;
	private bool m_sprintOnCD = false;
	private bool m_poisonOnCD = false;
	private float m_sprintCD=10f;
	private bool m_sprinting = false;
	private float m_poisonCD = 6f;


	void Awake(){
		m_attackState = Animator.StringToHash("Base.Attack");
		m_controller = GetComponent<CharacterController>();
		m_animationController = GetComponent<Animator>();
		Camera.main.GetComponent<CameraController>().m_target = transform;
		if(m_weaponHitBoxes != null) {
			//m_weaponHitBoxes.enabled = false;
		}
	}

	void Update(){
		if(!m_disableMovement) {
		 //currentBaseState = m_animationController.GetCurrentAnimatorStateInfo(0);
		m_moveStatus = "idle";
		
=======



	void Awake(){

		m_controller = GetComponent<CharacterController>();
		m_animationController = GetComponent<Animator>();
		Camera.main.GetComponent<BS_ThirdPersonCamera>().Target = transform;
		
	}

	void Update(){
		m_moveStatus = "idle";
		m_isWalking = m_walkByDefault;

		if(Input.GetAxis("Run") != 0){
			m_isWalking = !m_walkByDefault;
		}

>>>>>>> undead-defense:Assets/BloodSport/BS_Scripts/RPGCharacterController.cs
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
<<<<<<< HEAD:Assets/Game_BloodSport/Scripts/Character/Rogue/RogueController.cs
				m_jumping = true;
				m_moveDirection.y = m_jumpSpeed;
				m_animationController.SetBool("isJumping",true);
			}
			else{
				m_animationController.SetBool("isJumping",false);
			}

			if(m_moveDirection.magnitude > 0.05f){
				m_animationController.SetBool("isWalking",true);
			}else{
				m_animationController.SetBool("isWalking",false);
			}
			m_animationController.SetFloat("Speed", m_moveDirection.z);
			m_animationController.SetFloat("Direction", m_moveDirection.x);
=======
				m_animationController.SetTrigger("Jump");
				m_moveDirection.y = m_jumpSpeed;
			}

			if(m_moveDirection.magnitude > 0.1f){
				m_animationController.SetBool("isRunning",true);
			}else{
				m_animationController.SetBool("isRunning",false);
			}
			m_animationController.SetFloat("Forward", m_moveDirection.z);
			m_animationController.SetFloat("Right", m_moveDirection.x);
>>>>>>> undead-defense:Assets/BloodSport/BS_Scripts/RPGCharacterController.cs

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
<<<<<<< HEAD:Assets/Game_BloodSport/Scripts/Character/Rogue/RogueController.cs
		}


	if(Input.GetMouseButtonDown(0)){
=======
			m_animationController.SetTrigger("Jump");
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
>>>>>>> undead-defense:Assets/BloodSport/BS_Scripts/RPGCharacterController.cs
			m_animationController.SetBool("isAttacking",true);
			//m_weaponHitBox.enabled = true;
		}
		else if(Input.GetMouseButtonUp(0)){
			m_animationController.SetBool("isAttacking",false);
			//m_weaponHitBox.enabled = false;
		}
<<<<<<< HEAD:Assets/Game_BloodSport/Scripts/Character/Rogue/RogueController.cs

		
		
	
		
		
	if(m_sprinting){
		m_animationController.SetBool("isSprinting",true);
		m_isWalking = !m_walkByDefault;
	}
	else{
		m_animationController.SetBool("isSprinting",false);
		m_isWalking = m_walkByDefault;
		}
		

		}
	}
	
	public void ShadowStep(Transform targetLocation){
		gameObject.SetActive(false);
		gameObject.transform.position = targetLocation.position;
		gameObject.SetActive(true);
		m_targetGUI.SetActive(false);
	}
	
	
=======
		

	}
>>>>>>> undead-defense:Assets/BloodSport/BS_Scripts/RPGCharacterController.cs
}
