using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Warrior : MonoBehaviour {

	public Animator m_animator;
	private BS_Health m_healthScript;

	void Start() {
		m_animator = GetComponent<Animator>();
		m_healthScript = GetComponent<BS_Health>();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.X)) {
			m_healthScript.TakeDamage(20.0f);
			Debug.Log(m_healthScript.m_currentHealth);
		}
	}
}
