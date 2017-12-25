using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController_UD : MonoBehaviour {

	public int m_goldValue = 10;
	public float m_chasingTime = 5f;
	public float m_playerOffset = 3f;
	public float m_attackRate = 3f;
	public int numAttackAnimations = 1;
	public int numDeathAnimations = 1;
	public int numWalkAnimations = 1;
	public int numHitAnimations = 1;
	
	enum m_states {IDLE,MOVING_TO_WP,CHASING_PLAYER,ATTACKING,STUNNED,SLOWED,DEAD}
	private Health_UD m_health;
	private NavWaypointAI_UD m_movement;
	private Animator m_anim;
	private Sensor_UD m_sensor;
	private WeaponScript m_weapon;
	private m_states m_currentState = m_states.IDLE;
	private Transform m_target;
	private bool m_canAttack = true;
	private bool m_isAttacking = false;
	private bool m_chasingPlayer = false;
	private int attackAnimInt;
	private int deathAnimInt;
	private int hitAnimInt;
	private int walkAnimInt;
	private List<int> m_attackStates = new List<int>();

	private void Start() {
		m_anim = GetComponent<Animator>();
		m_health = GetComponent<Health_UD>();
		m_movement = GetComponent<NavWaypointAI_UD>();
		m_sensor = GetComponentInChildren<Sensor_UD>();
		m_weapon = GetComponentInChildren<WeaponScript>();
		attackAnimInt = Random.Range( 1, numAttackAnimations+1);
		deathAnimInt = Random.Range( 1, numDeathAnimations+1);
		hitAnimInt = Random.Range( 1, numHitAnimations+1);
		walkAnimInt = Random.Range( 1, numWalkAnimations+1);
		m_attackStates.Add(Animator.StringToHash("BaseLayer.Attack1"));
		m_attackStates.Add(Animator.StringToHash("BaseLayer.Attack2"));
		m_attackStates.Add(Animator.StringToHash("BaseLayer.Attack3"));
	}

	private void Update() {
		HandleHealth();
		CheckForEnemies();
		StateUpdate();
	}

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

	private void HandleHealth() {
		if (m_health.CheckIfDead()) {
			SetNewState(m_states.DEAD);
		}
	}

	private void SetNewState(m_states newState) {
		Debug.Log("State changed from " + m_currentState + " to " + newState);
		ExitState();
		EnterState(newState);
	}

	private void ExitState() {
		switch (m_currentState) {
			case m_states.IDLE:

			break;
			case m_states.MOVING_TO_WP:
				m_movement.StopMovement();
				m_movement.enabled = false;
				m_anim.SetInteger("WalkState", 0);		
			break;
			case m_states.CHASING_PLAYER:
				m_movement.StopMovement();
				m_movement.enabled = false;
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
				m_anim.SetInteger("DeathState", 0);
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
				m_movement.enabled = true;
				m_movement.MoveToWP();
				m_anim.SetInteger("WalkState", walkAnimInt);
			break;
			case m_states.CHASING_PLAYER:
				m_movement.enabled = true;
				m_anim.SetInteger("WalkState", walkAnimInt);
				if (!m_chasingPlayer) {
					StartCoroutine(StopChasing());
					m_chasingPlayer = true;
				}
			break;
			case m_states.ATTACKING:
				m_canAttack = false;
				m_anim.SetInteger("AttackState", attackAnimInt);
				StartCoroutine(DelayBetweenAttacks());
			break;
			case m_states.STUNNED:
				m_anim.SetTrigger("Hit");
			break;
			case m_states.SLOWED:

			break;
			case m_states.DEAD:
				GameManager_UD.instance.AddGold(m_goldValue);
				WaveManager.instance.EnemyDied();
				m_movement.Reset();
				m_anim.SetInteger("DeathState", deathAnimInt);
			break;
			default:
				Debug.LogError("Unknown state");
			break;
		}
	}

	private void CheckAttackState() {
//		m_anim.GetCurrentAnimatorStateInfo
	}

	private void HandleAnimationStates(AnimatorStateInfo animState) {
		if (m_attackStates.Contains(animState.fullPathHash) && !m_isAttacking) {
			m_isAttacking = true;
		} else if (!m_attackStates.Contains(animState.fullPathHash) && m_isAttacking) {
			SetNewState(m_states.IDLE);
		}
	}

	private void CheckForEnemies() {
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

	private bool CanAttackPlayer() {
		bool retBool = false;
		if (Vector3.Distance(transform.position, m_target.position) < m_playerOffset) {
			retBool = true;
		}
		return retBool;
	}

	IEnumerator ResetEnemy() {
		for (float t = 0; t < 2; t += Time.deltaTime) {
			yield return null;
		}
		m_health.Die();
		m_anim.Rebind();
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

	private void FaceTarget(Transform target) {
		Vector3 tempTargetPos = target.transform.position;
		tempTargetPos.y = 0;
		Vector3 tempPos = transform.position;
		tempPos.y = 0;
		Vector3 targetDir = tempTargetPos - tempPos;
		Quaternion rotation = Quaternion.LookRotation(targetDir);
		transform.rotation = rotation;
	}

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
