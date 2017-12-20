using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController_UD : MonoBehaviour {

	public int m_goldValue = 10;
	public float m_chasingTime = 3f;
	
	enum m_states {IDLE,MOVING_TO_WP,CHASING_PLAYER,ATTACKING,STUNNED,SLOWED,DEAD}
	private Health_UD m_health;
	private NavWaypointAI_UD m_movement;
	// private WEAPONSCRIPT m_weapon;
	// private ANIMATIONSCRIPT m_anim;
	private Animator m_anim;
	private Sensor_UD m_sensor;
	private m_states m_currentState = m_states.MOVING_TO_WP;
	private Transform m_target;
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

	private void StateUpdate() {
		switch (m_currentState) {
			case m_states.IDLE:
				if (m_isAttacking) {
					m_currentState = m_states.ATTACKING;
				}
			break;
			case m_states.MOVING_TO_WP:
				m_movement.Move();
			break;
			case m_states.CHASING_PLAYER:

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
		ExitState();
		EnterState(newState);
	}

	private void ExitState() {
		switch (m_currentState) {
			case m_states.IDLE:

			break;
			case m_states.MOVING_TO_WP:
			
			break;
			case m_states.CHASING_PLAYER:
				m_anim.SetBool("Attack", false);
			break;
			case m_states.ATTACKING:
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
				if (m_isAttacking) {
					m_currentState = m_states.ATTACKING;
				}
			break;
			case m_states.MOVING_TO_WP:
				m_movement.Move();
			break;
			case m_states.CHASING_PLAYER:
				m_anim.SetBool("Attack", true);
			break;
			case m_states.ATTACKING:
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
		if (m_sensor.Targets.Count > 0) {
			if (m_sensor.m_playerBase != null) {

			}
			m_target = m_sensor.GetClosestEnemy().transform;
			SetNewState(m_states.CHASING_PLAYER);
		}
	}

	IEnumerator StopChasing() {
		yield return new WaitForSeconds(m_chasingTime);
		SetNewState(m_states.MOVING_TO_WP);
	}
}
