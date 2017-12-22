using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipCanvas : MonoBehaviour {

	public Text m_abilityDescriptionTitle;
	public Text m_abilityDescription;
	public string m_movementTitle;
	public string m_damageTitle;
	public string m_ultimateTitle;
	public string m_passiveTitle;
	public string m_movementDescription;
	public string m_damageDescription;
	public string m_ultimateDescription;
	public string m_passiveDescription;
	public Canvas m_toolTipCanvas;

	void Start() {
		m_toolTipCanvas.enabled = false;
	}

	public void OnMouseOverMovement() {
		m_toolTipCanvas.enabled = true;
		m_abilityDescriptionTitle.text = m_movementTitle;
		m_abilityDescription.text = m_movementDescription;
	}

	public void OnMouseOverAbility() {
		m_toolTipCanvas.enabled = true;
		m_abilityDescriptionTitle.text = m_damageTitle;
		m_abilityDescription.text = m_damageDescription;
	}

	public void OnMouseOverUltimate() {
		m_toolTipCanvas.enabled = true;
		m_abilityDescriptionTitle.text = m_ultimateTitle;
		m_abilityDescription.text = m_ultimateDescription;
	}

	public void OnMouseOverPassive() {
		m_toolTipCanvas.enabled = true;
		m_abilityDescriptionTitle.text = m_passiveTitle;
		m_abilityDescription.text = m_passiveDescription;
	}

	public void HideToolTipCanvas() {
		m_toolTipCanvas.enabled = false;
	}
}
