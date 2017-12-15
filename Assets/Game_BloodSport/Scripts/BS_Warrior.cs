using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Warrior : MonoBehaviour {

	public float m_chargeSpeed = 200.0f;
	public Collider m_shieldCollider;
	public float m_chargeTurnSpeed = 50.0f;
	public bool m_charging = false;
	public bool m_usingWhirlWind = false;
	public bool m_usingUltimate = false;
	public GameObject m_normalShield;
	public GameObject m_ultimateShield;

	private BS_Health m_healthScript;
	private RPGCharacterController m_characterController;
	private Animator m_animator;
	private float m_previousTurnSpeed;

	void Start() {
		m_animator = GetComponent<Animator>();
		m_healthScript = GetComponent<BS_Health>();
		m_characterController = GetComponent<RPGCharacterController>();
		m_shieldCollider.enabled = false;
		m_previousTurnSpeed = m_characterController.m_turnSpeed;
		m_ultimateShield.SetActive(false);
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.C) && !m_usingUltimate) {
			Charge();
		} else if (Input.GetKeyDown(KeyCode.V) && !m_usingUltimate) {
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
		m_characterController.m_turnSpeed = m_chargeTurnSpeed;
	}

	public void ResetAfterCharge() {
		m_charging = false;
		m_characterController.m_disableMovement = false;
		m_animator.SetBool("isCharging", false);
		m_shieldCollider.enabled = false;
		m_characterController.m_turnSpeed = m_previousTurnSpeed;
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
		m_characterController.m_hasDealtDamage = false;
	}

	public void Ultimate() {
		m_animator.SetBool("isUlting", true);
		m_usingUltimate = true;
		m_normalShield.SetActive(false);
		m_ultimateShield.SetActive(true);
	}

	public void ResetAfterUltimate() {
		m_animator.SetBool("isUlting", false);
		m_usingUltimate = false;
		m_normalShield.SetActive(true);
		m_ultimateShield.SetActive(false);
	}
}
