using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController_UD : MonoBehaviour {

	#region External Variables
	public int m_goldValue = 10;
	public float m_chasingTime = 5f; //time the enemy chases the player for
	public float m_playerOffset = 3f; //how far the enemy is from the player before it attacks
	public float m_attackRate = 3f;	
	public int numDeathAnimations = 1;
	public int numWalkAnimations = 1;
	public int numHitAnimations = 1;
	#endregion
	
	#region Internal Variables
	enum m_states {IDLE,MOVING_TO_WP,CHASING_PLAYER,ATTACKING,STUNNED,SLOWED,DEAD}
	private Health_UD m_health;
	private NavWaypointAI_UD m_movement;
	private Animator m_anim;
	private Sensor_UD m_sensor;
	private WeaponScript m_weapon;
	private m_states m_currentState = m_states.IDLE;
	private Transform m_target;
	private Collider m_col;
	private bool m_startAnimDelay = false;
	private bool m_canAttack = true;
	private bool m_isAttacking = false;
	private bool m_chasingPlayer = false;
	private const int NUM_ATTACK_ANIMS = 4;
	private int deathAnimInt;
	private int hitAnimInt;
	private int walkAnimInt;
	private NavMeshAgent m_agent;
	private Rigidbody m_rb;
	private List<int> m_attackStates = new List<int>();
	#endregion

	#region Standard Methods
	private void Start() {
		m_anim = GetComponent<Animator>();
		m_health = GetComponent<Health_UD>();
		m_movement = GetComponent<NavWaypointAI_UD>();
		m_sensor = GetComponentInChildren<Sensor_UD>();
		m_weapon = GetComponentInChildren<WeaponScript>();
		m_agent = GetComponent<NavMeshAgent>();
		m_rb = GetComponent<Rigidbody>();
		m_col = GetComponent<Collider>();
		deathAnimInt = Random.Range( 1, numDeathAnimations+1);
		hitAnimInt = Random.Range( 1, numHitAnimations+1);
		walkAnimInt = Random.Range( 1, numWalkAnimations+1);
		m_movement.SetSpeed(walkAnimInt);
		m_attackStates.Add(Animator.StringToHash("BaseLayer.Attack1"));
		m_attackStates.Add(Animator.StringToHash("BaseLayer.Attack2"));
		m_attackStates.Add(Animator.StringToHash("BaseLayer.Attack3"));
	}

	private void Update() {
		HandleHealth();
		CheckForEnemies();
		StateUpdate();
	}
	#endregion

	#region States
	private void StateUpdate() {
		AnimatorStateInfo currentBaseLayerState = m_anim.GetCurrentAnimatorStateInfo(0);
		switch (m_currentState) {
			case m_states.IDLE:
				if (m_canAttack && m_target != null) {
					if (m_target.tag == "PlayerBase") {
						SetNewState(m_states.ATTACKING);
					} else {
						SetNewState(m_states.CHASING_PLAYER);
					}
				} else if (m_movement.Velocity.magnitude > 0.5 && m_target == null) {
					SetNewState(m_states.MOVING_TO_WP);
				}
			break;
			case m_states.MOVING_TO_WP:
				m_movement.Move();
			break;
			case m_states.CHASING_PLAYER:
				m_movement.ChaseTarget(m_target);
				if (CanAttackPlayer()) {
					SetNewState(m_states.ATTACKING);
				}
			break;
			case m_states.ATTACKING:
				HandleAnimationStates(currentBaseLayerState);
			break;
			case m_states.STUNNED:

			break;
			case m_states.SLOWED:

			break;
			case m_states.DEAD:

			break;
			default:
				Debug.LogError("Unknown state");
			break;
		}
	}

	private void ExitState() {
		switch (m_currentState) {
			case m_states.IDLE:

			break;
			case m_states.MOVING_TO_WP:
				m_movement.StopMovement();
				m_anim.SetInteger("WalkState", 0);		
			break;
			case m_states.CHASING_PLAYER:
				m_movement.StopMovement();
				m_anim.SetInteger("WalkState", 0);
			break;
			case m_states.ATTACKING:
				m_isAttacking = false;
				m_anim.SetInteger("AttackState", 0);
			break;
			case m_states.STUNNED:
				
			break;
			case m_states.SLOWED:

			break;
			case m_states.DEAD:
				m_agent.enabled = true;
				m_rb.useGravity = true;
				m_col.enabled = true;
				m_target = null;
				ResetBools();
				m_movement.Reset();
				m_sensor.RessetSensor();
				m_anim.Rebind();
				m_health.Die();			
			break; 
			default:
				Debug.LogError("Unknown state");
			break;
		}
	}

	private void EnterState(m_states newState) {
		m_currentState = newState;
		switch (m_currentState) {
			case m_states.IDLE:

			break;
			case m_states.MOVING_TO_WP:
				m_movement.StartMovement();
				m_movement.MoveToWP();
				if (m_startAnimDelay) {
					SetWalkAnim();
				} else {
					Invoke("SetWalkAnim",Random.Range(0,1f));
					m_startAnimDelay = true;
				}
			break;
			case m_states.CHASING_PLAYER:
				m_movement.StartMovement();
				m_anim.SetInteger("WalkState", walkAnimInt);
				if (!m_chasingPlayer) {
					StartCoroutine(StopChasing());
					m_chasingPlayer = true;
				}
			break;
			case m_states.ATTACKING:
				m_canAttack = false;
				m_anim.SetInteger("AttackState", Random.Range( 1, NUM_ATTACK_ANIMS));
				StartCoroutine(DelayBetweenAttacks());
			break;
			case m_states.STUNNED:
				m_anim.SetTrigger("Hit");
			break;
			case m_states.SLOWED:

			break;
			case m_states.DEAD:
				m_col.enabled = false;
				GameManager_UD.instance.AddGold(m_goldValue);
				WaveManager.instance.EnemyDied();
				m_anim.SetInteger("DeathState", deathAnimInt);
				StartCoroutine(ResetEnemy());
			break;
			default:
				Debug.LogError("Unknown state");
			break;
		}
	}

	private void SetNewState(m_states newState) {
		Debug.Log("State changed from " + m_currentState + " to " + newState);
		ExitState();
		EnterState(newState);
	}
	#endregion

	#region Custom Methods
	private void HandleHealth() {
		if (m_health.CheckIfDead() && m_currentState != m_states.DEAD) {
			SetNewState(m_states.DEAD);
		}
	}

	private void HandleAnimationStates(AnimatorStateInfo animState) {
		if (m_attackStates.Contains(animState.fullPathHash) && !m_isAttacking) {
			m_isAttacking = true;
		} else if (!m_attackStates.Contains(animState.fullPathHash) && m_isAttacking) {
			SetNewState(m_states.IDLE);
		}
	}

	private void CheckForEnemies() {
		if (!m_health.CheckIfDead()) {
			if (m_sensor.m_playerBase != null && m_target == null) {
				m_target = m_sensor.m_playerBase.transform;
				SetNewState(m_states.ATTACKING);
			}
			if (m_sensor.Targets.Count > 0 && m_target == null) {
				m_target = m_sensor.GetFirstEnemy().transform;
				SetNewState(m_states.CHASING_PLAYER);
			}
			if (m_target != null) {
				FaceTarget(m_target);
			}
		}
	}

	private bool CanAttackPlayer() {
		bool retBool = false;
		if (Vector3.Distance(transform.position, m_target.position) < m_playerOffset) {
			retBool = true;
		}
		return retBool;
	}

	private void SetWalkAnim() {
		m_anim.SetInteger("WalkState", walkAnimInt);
	}

	private void ResetBools() {
		m_startAnimDelay = false;
		m_canAttack = true;
		m_isAttacking = false;
		m_chasingPlayer = false;
	}

	private void FaceTarget(Transform target) {
		Vector3 tempTargetPos = target.transform.position;
		tempTargetPos.y = 0;
		Vector3 tempPos = transform.position;
		tempPos.y = 0;
		Vector3 targetDir = tempTargetPos - tempPos;
		Quaternion rotation = Quaternion.LookRotation(targetDir);
		transform.rotation = rotation;
	}
	#endregion

	#region Coroutines
	IEnumerator ResetEnemy() {
		m_agent.velocity = Vector3.zero;
		m_agent.enabled = false;
		m_rb.useGravity = false;
		m_rb.velocity = Vector3.zero;
		yield return new WaitForSeconds(5);
		for (float t = 0; t < 3; t += Time.deltaTime) {
			transform.Translate(0,-0.005f,0);
			yield return null;
		}
		SetNewState(m_states.IDLE);
	}

	IEnumerator DelayBetweenAttacks() {
		yield return new WaitForSeconds(m_attackRate);
		m_canAttack = true;
	}

	IEnumerator StopChasing() {
		yield return new WaitForSeconds(m_chasingTime);
		m_target = null;
		while (m_currentState == m_states.ATTACKING) {
			yield return null;
		}
		if (m_currentState == m_states.CHASING_PLAYER) {
			m_target = null;
			m_chasingPlayer = false;
			SetNewState(m_states.MOVING_TO_WP);
		}
	}
	#endregion

	#region AnimationEventMethods
	public void HitBoxOn() {
		m_weapon.m_dealDamage = true;
	}

	public void HitBoxOff() {
		m_weapon.m_dealDamage = false;
		m_weapon.Clear();
	}	
	#endregion
}
