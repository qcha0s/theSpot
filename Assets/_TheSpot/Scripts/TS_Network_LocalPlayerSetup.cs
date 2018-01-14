using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class TS_Network_LocalPlayerSetup : NetworkBehaviour {

	[SyncVar]
	public string m_PlayerName = null;
	public Camera PlayerCamera { get{ return m_cam; } }
	private NetworkInstanceId m_playerNetID;
	private FirstPersonController m_movement;
	private AudioListener m_audio;
	private Camera m_cam;
	private TS_Network_Chat m_chat;
	private TS_InGameUIManager m_UI;
	private RayCastInteraction_UD m_interaction;
	[SyncVar]
	private int m_activeIndex = -1;
	public Text m_nameText;
	public GameObject m_chatSystem;
	public GameObject m_EventSystem;
	public GameObject m_glasses;
	[Serializable]
	public class Character {
		[SerializeField]
		public Avatar m_avatar;
		[SerializeField]
		public GameObject m_character;
	}
	public Character[] m_characters;
	private static Transform m_camTransform = null; //for billboard effect


	public override void OnStartLocalPlayer() {
		m_movement = GetComponent<FirstPersonController>();
		m_audio = GetComponentInChildren<AudioListener>();
		m_cam = GetComponentInChildren<Camera>();
		m_chat = GetComponentInChildren<TS_Network_Chat>();
		m_UI = GetComponent<TS_InGameUIManager>();
		m_interaction = GetComponent<RayCastInteraction_UD>();
		m_movement.enabled = true;
		m_audio.enabled = true;
		m_cam.enabled = true;
		m_chat.enabled = true;
		m_UI.enabled = true;
		m_interaction.enabled = true;
		m_camTransform = m_cam.transform;
		m_chatSystem.SetActive(true);
		m_EventSystem.SetActive(true);
		m_glasses.SetActive(false);
		TS_CustomNetworkManager.Instance.m_localPlayer = gameObject;
		m_activeIndex = UnityEngine.Random.Range(0,m_characters.Length);
		CmdTellServerMySecrets(m_activeIndex,PlayerPrefs.GetString("PlayerName"));
		ActivateGameObject();
	}

	private void Update() {
		if (m_PlayerName != null && (transform.name == "Networked_Player(Clone)" || transform.name == "")) {
			transform.name = m_PlayerName;
			if (isLocalPlayer) {
				m_nameText.enabled = false;
				m_chat.enabled = true;
				m_chat.SetPlayerName(m_PlayerName);
			} else {
				m_nameText.text = m_PlayerName;				
			}
		}
		if (!isLocalPlayer) {
			if (m_activeIndex >= 0 && !m_characters[m_activeIndex].m_character.activeInHierarchy) {
				m_characters[m_activeIndex].m_character.SetActive(true);
				GetComponent<Animator>().avatar = m_characters[m_activeIndex].m_avatar;				
			}
		}
		RotateTextToFaceCamera();
	}

	private void RotateTextToFaceCamera() {
		if (m_camTransform != null) {
			m_nameText.transform.LookAt(m_camTransform);
			Vector3 rot = m_nameText.transform.rotation.eulerAngles;
			rot.y += 180;
			m_nameText.transform.eulerAngles = rot;
		}
	}

	private void ActivateGameObject() {
		m_characters[m_activeIndex].m_character.SetActive(true);
		GetComponent<Animator>().avatar = m_characters[m_activeIndex].m_avatar;
	}

	[Command]
	void CmdTellServerMySecrets(int index, string name) {
		m_activeIndex = index;
		m_PlayerName = name;
	}
}
