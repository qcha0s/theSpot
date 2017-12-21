using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController_UD : MonoBehaviour {

	public int m_goldValue = 10;
	public float m_chasingTime = 5f;
	public float m_baseOffset = 3f;
	public float m_attackRate = 2f;
	
	enum m_states {IDLE,MOVING_TO_WP,CHASING_PLAYER,ATTACKING,STUNNED,SLOWED,DEAD}
	private Health_UD m_health;
	private NavWaypointAI_UD m_movement;
	private Animator m_anim;
	private Sensor_UD m_sensor;
	private m_states m_currentState = m_states.MOVING_TO_WP;
	private Transform m_target;
	private bool m_canAttack = true;
	private bool m_isAttacking = false;

	private void Start() {
		m_anim = GetComponentInChildren<Animator>();
		m_health = GetComponent<Health_UD>();
		m_movement = GetComponent<NavWaypointAI_UD>();
		m_sensor = GetComponentInChildren<Sensor_UD>();
	}

	private void Update() {
		HandleHealth();
		StateUpdate();
	}

	void FixedUpdate() {
		CheckForEnemies();
	}

	private void StateUpdate() {
		switch (m_currentState) {
			case m_states.IDLE:
				if (m_canAttack) {
					if (m_target.tag == "PlayerBase") {
						SetNewState(m_states.ATTACKING);
					} else {
						SetNewState(m_states.CHASING_PLAYER);
					}
				}
			break;
			case m_states.MOVING_TO_WP:
				m_movement.Move();
			break;
			case m_states.CHASING_PLAYER:
				m_movement.ChasePlayer(m_target);
			break;
			case m_states.ATTACKING:
				
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
			break;
			case m_states.CHASING_PLAYER:
				m_anim.SetBool("Attack", false);
			break;
			case m_states.ATTACKING:
				m_isAttacking = false;
				m_anim.SetBool("Attack", false);
			break;
			case m_states.STUNNED:
				
			break;
			case m_states.SLOWED:

			break;
			case m_states.DEAD:
				m_anim.SetBool("Dead", false);
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
				if (m_canAttack) {
					m_currentState = m_states.ATTACKING;
				}
			break;
			case m_states.MOVING_TO_WP:
				m_movement.MoveToWP();
			break;
			case m_states.CHASING_PLAYER:
				StartCoroutine(StopChasing());
			break;
			case m_states.ATTACKING:
				m_isAttacking = true;
				m_canAttack = false;
				m_anim.SetBool("Attack", true);
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
				m_currentState = m_states.MOVING_TO_WP;
				m_health.Die();
				m_anim.SetBool("Dead", true);
			break;
			default:
				Debug.LogError("Unknown state");
			break;
		}
	}

	private void CheckForEnemies() {
		Debug.Log(m_sensor.m_playerBase == null);
		if (m_sensor.m_playerBase != null && m_target == null) {
			m_target = m_sensor.m_playerBase.transform;
			SetNewState(m_states.ATTACKING);
		}
		if (m_sensor.Targets.Count > 0 && m_target == null) {
			m_target = m_sensor.GetFirstEnemy().transform;
			SetNewState(m_states.CHASING_PLAYER);
		}
	}

	public void StopAttacking() {
		if (m_target.tag == "PlayerBase") {
			SetNewState(m_states.IDLE);
		} else {
			SetNewState(m_states.CHASING_PLAYER);
		}
	}

	IEnumerator CanDelayBeforeAttack() {
		yield return new WaitForSeconds(m_attackRate);
		m_canAttack = true;
	}

	IEnumerator StopChasing() {
		yield return new WaitForSeconds(m_chasingTime);
		m_target = null;
		while (m_currentState == m_states.ATTACKING) {
			yield return null;
		}
		SetNewState(m_states.MOVING_TO_WP);
	}
}
