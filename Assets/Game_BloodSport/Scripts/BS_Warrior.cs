using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Warrior : MonoBehaviour {

	public float m_chargeSpeed = 200.0f;
	public Collider m_shieldCollider;
	private BS_Health m_healthScript;
	private RPGCharacterController m_characterController;
	private bool m_charging = false;
	private bool m_usingWhirlWind = false;
	private bool m_usingUltimate = false;
	private Animator m_animator;

	void Start() {
		m_animator = GetComponent<Animator>();
		m_healthScript = GetComponent<BS_Health>();
		m_characterController = GetComponent<RPGCharacterController>();
		m_shieldCollider.enabled = false;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.C)) {
			Charge();
		} else if (Input.GetKeyDown(KeyCode.V)) {
			WhirlWind();
		} else if (Input.GetKeyDown(KeyCode.R)) {
			Ultimate();
		}

		if(m_charging) {
			transform.position += transform.forward * Time.deltaTime * m_chargeSpeed;
		}
	}

	public void Charge() {
		m_charging = true;
		m_characterController.m_disableMovement = true;
		m_animator.SetBool("isCharging", true);
		m_shieldCollider.enabled = true;
	}

	public void ResetAfterCharge() {
		m_charging = false;
		m_characterController.m_disableMovement = false;
		m_animator.SetBool("isCharging", false);
		m_shieldCollider.enabled = false;
	}

	public void WhirlWind() {
		m_usingWhirlWind = true;
		m_animator.SetBool("isWhirlwind", true);
		m_characterController.m_weaponHitBoxes.enabled = true;
	}

	public void ResetAfterWhirlWind() {
		m_usingWhirlWind = false;
		m_animator.SetBool("isWhirlwind", false);
		m_characterController.m_weaponHitBoxes.enabled = false;
	}

	public void Ultimate() {
		m_usingUltimate = true;
	}

	public void ResetAfterUltimate() {
		m_usingUltimate = false;
	}
}
