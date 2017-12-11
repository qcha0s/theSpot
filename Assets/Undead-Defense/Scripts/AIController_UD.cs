using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController_UD : MonoBehaviour {
	
	enum m_states {IDLE,MOVING_TO_WP,CHASING_PLAYER,ATTACKING,STUNNED,SLOWED,DEAD}
	private Health_UD m_health;
	private NavWaypointAI_UD m_movement;
	// private WEAPONSCRIPT m_weapon;
	// private ANIMATIONSCRIPT m_anim;
	private m_states m_currentState = m_states.IDLE;
	private Transform m_target;

	private void Start() {
		m_health = GetComponent<Health_UD>();
		m_movement = GetComponent<NavWaypointAI_UD>();
	}

	private void Update() {
		switch (m_currentState) {
			case m_states.IDLE:

			break;
			case m_states.MOVING_TO_WP:

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
}
