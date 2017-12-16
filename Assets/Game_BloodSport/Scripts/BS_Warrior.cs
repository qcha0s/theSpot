using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Warrior : MonoBehaviour {

	public float m_chargeSpeed = 200.0f;
	public Collider m_shieldCollider;
	public float m_chargeTurnSpeed = 50.0f;
	public bool m_charging = false;
	public bool m_usingWhirlWind = false;
	public bool m_usingUltimate = false;
	public GameObject m_normalShield;
	public GameObject m_ultimateShield;
	public float m_whirlwindCD = 5.0f;
	public float m_chargeCD = 10.0f;
	public float m_ultimateDuration = 5.0f;
	public Text m_abilityDescription;
	public Text m_abilityDescriptionTitle;
	public string m_chargeDescription;
	public string m_whirlwindDescription;
	public string m_ultimateDescription;
	public string m_passiveDescription;
	public Canvas m_toolTipCanvas;

	private BS_Health m_healthScript;
	private RPGCharacterController m_characterController;
	private Animator m_animator;
	private float m_previousTurnSpeed;
	private bool m_whirlwindOnCD = false;
	private bool m_ChargeOnCD = false;

	void Start() {
		m_animator = GetComponent<Animator>();
		m_healthScript = GetComponent<BS_Health>();
		m_characterController = GetComponent<RPGCharacterController>();
		m_shieldCollider.enabled = false;
		m_toolTipCanvas.enabled = false;
		m_previousTurnSpeed = m_characterController.m_turnSpeed;
		m_ultimateShield.SetActive(false);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1) && !m_usingUltimate && !m_whirlwindOnCD) {
			WhirlWind();
		} else if(Input.GetKeyDown(KeyCode.Alpha2) && !m_usingUltimate && !m_ChargeOnCD) {
			Charge();
		} else if (Input.GetKeyDown(KeyCode.Alpha3) && !m_usingUltimate) {
			Ultimate();
		}

		if(m_charging) {
			transform.position += transform.forward * Time.deltaTime * m_chargeSpeed;
		}
	}

	public void Charge() {
		if(!m_charging) {
			m_ChargeOnCD = true;
			StartCoroutine(CoolDownSystem(m_chargeCD, "Charge"));
			m_charging = true;
			m_characterController.m_disableMovement = true;
			m_animator.SetBool("isCharging", true);
			m_shieldCollider.enabled = true;
			m_characterController.m_turnSpeed = m_chargeTurnSpeed;
		}
	}

	public void ResetAfterCharge() {
		m_charging = false;
		m_characterController.m_disableMovement = false;
		m_animator.SetBool("isCharging", false);
		m_shieldCollider.enabled = false;
		m_characterController.m_turnSpeed = m_previousTurnSpeed;
	}

	public void WhirlWind() {
		if(!m_usingWhirlWind) {
			m_whirlwindOnCD = true;
			StartCoroutine(CoolDownSystem(m_whirlwindCD, "Whirlwind"));
			m_usingWhirlWind = true;
			m_animator.SetBool("isWhirlwind", true);
			m_characterController.m_weaponHitBoxes.enabled = true;
		}
	}

	public void ResetAfterWhirlWind() {
		m_usingWhirlWind = false;
		m_animator.SetBool("isWhirlwind", false);
		m_characterController.m_weaponHitBoxes.enabled = false;
		m_characterController.m_hasDealtDamage = false;
	}

	public void Ultimate() {
		if(!m_usingUltimate) {
			StartCoroutine(CoolDownSystem(m_ultimateDuration, "Ultimate"));
			m_animator.SetBool("isUlting", true);
			m_usingUltimate = true;
			m_normalShield.SetActive(false);
			m_ultimateShield.SetActive(true);
		}
	}

	public void ResetAfterUltimate() {
		m_animator.SetBool("isUlting", false);
		m_usingUltimate = false;
		m_normalShield.SetActive(true);
		m_ultimateShield.SetActive(false);
	}

	public void OnMouseOverMovement() {
		m_toolTipCanvas.enabled = true;
		m_abilityDescriptionTitle.text = "Charge";
		m_abilityDescription.text = m_chargeDescription;
	}

	public void OnMouseOverAbility() {
		m_toolTipCanvas.enabled = true;
		m_abilityDescriptionTitle.text = "Whirlwind";
		m_abilityDescription.text = m_whirlwindDescription;
	}

	public void OnMouseOverUltimate() {
		m_toolTipCanvas.enabled = true;
		m_abilityDescriptionTitle.text = "Shield Wall";
		m_abilityDescription.text = m_ultimateDescription;
	}

	public void OnMouseOverPassive() {
		m_toolTipCanvas.enabled = true;
		m_abilityDescriptionTitle.text = "Iron Man";
		m_abilityDescription.text = m_passiveDescription;
	}

	public void HideToolTipCanvas() {
		m_toolTipCanvas.enabled = false;
	}

	IEnumerator CoolDownSystem(float cooldownvalue, string Ability){	
		yield return new WaitForSeconds(cooldownvalue);
		if(Ability == "Whirlwind"){
			m_whirlwindOnCD = false;
		}
		if(Ability == "Charge"){
			m_ChargeOnCD = false;
		}
		if(Ability == "Ultimate") {
			ResetAfterUltimate();
		}
	}
}
