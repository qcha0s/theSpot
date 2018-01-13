using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_SoundManager : MonoBehaviour {

	private AudioSource m_sound;
	private static TS_SoundManager m_instance = null;

	public static TS_SoundManager Instance { get { return m_instance; } }

	private void Awake() {
		if (m_instance != null && m_instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			m_instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start() {
		m_sound = GetComponent<AudioSource>();
		StartMusic();
	}

	public void StopMusic() {
		m_sound.Stop();
	}

	public void StartMusic() {
		m_sound.Play();
	}
}
