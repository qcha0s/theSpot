using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TS_MenuManager : MonoBehaviour {

	[Serializable]
	public class TS_Light {
		[SerializeField]
		public Renderer m_rend;
		[SerializeField]
		public Light m_light;
		[SerializeField]
		public Material m_offMat;
		[SerializeField]
		public Material m_onMat;
	}
	public Button m_joinGameButton;
	public int m_lightIntensity = 10;
	public float m_flickerDuration = 0.1f;
	public TS_Light[] m_arcadeLightsRight;
	public TS_Light[] m_arcadeLightsleft;
	private int m_numLights;
	private int m_activeLight = 0;

	private void Start() {
		m_numLights = m_arcadeLightsRight.Length;
		CheckNameValidity();
		StartCoroutine(StartLights());
	}

	public void PlayerNameInput(string name) {
		PlayerPrefs.SetString("PlayerName", name);
		CheckNameValidity();
	}

	public void StartGame() {
		TS_CustomNetworkManager.Instance.HostOrJoin();
		m_joinGameButton.interactable = false;
	}

	public void QuitGame() {
		Application.Quit();
	}

	private void CheckNameValidity() {
		if (PlayerPrefs.GetString("PlayerName") != null && PlayerPrefs.GetString("PlayerName") != "") {
			m_joinGameButton.interactable = true;
		} else {
			m_joinGameButton.interactable = false;
		}
	}

	IEnumerator StartLights() {
		for (;;) {
			for (float t = 0; t < 2*m_flickerDuration; t+=Time.deltaTime) {
				float lerp = Mathf.PingPong(t, m_flickerDuration) / m_flickerDuration;
				UpdateLights(m_arcadeLightsRight, lerp, t);
				UpdateLights(m_arcadeLightsleft, lerp, t);
				yield return null;		
			}
			if (m_activeLight < m_numLights-1) {
				m_activeLight++;
			} else {
				m_activeLight = 0;
			}
		}
	}

	private void UpdateLights( TS_Light[] lights, float lerp, float t) {
		lights[m_activeLight].m_rend.material.Lerp(lights[m_activeLight].m_offMat, lights[m_activeLight].m_onMat, lerp);	
		if (t < m_flickerDuration) {
			lights[m_activeLight].m_light.intensity = t*m_lightIntensity;
		} else {
			if (lights[m_activeLight].m_light.intensity < 1) {
				lights[m_activeLight].m_light.intensity = 0;
				lights[m_activeLight].m_rend.material = lights[m_activeLight].m_offMat;
			} else {
				lights[m_activeLight].m_light.intensity = (2 - (t/m_flickerDuration))*m_lightIntensity;
			}
		}
	}
}
