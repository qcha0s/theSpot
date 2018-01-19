using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioRandomPlay : MonoBehaviour {

	public AudioClip[] clips;
	
	//public AudioMixerGroup output;

	public float minPitch = .95f;
	public float maxPitch = 1.05f;
	
	private ItemKnockback m_Ik;
	public ScoreManager m_sm;

	void Awake(){
		m_Ik = GetComponent<ItemKnockback>();

		m_sm = GetComponent<ScoreManager>();
	}

	void Update () {

		if(m_Ik != null) { // give it time to initialize 
			
			if (m_Ik.IsHit() ) {
				Debug.Log("hit sound");
				PlaySound();
				m_Ik.Hit(false);
			}


		
		}

		if(m_sm != null) {

			if (m_sm.m_goal) {
				Debug.Log("goal sound");
				PlaySound();
				m_sm.m_goal = false;	
			}	

			if (m_sm.m_win) {
				Debug.Log("Win sound");
				PlaySound();
				m_sm.m_win = false;
			}
		}
	}

	public void Play (string name) {
		PlaySound();
	}

	void PlaySound() {
		int randomClip = Random.Range (0, clips.Length);

		AudioSource source = gameObject.AddComponent<AudioSource>();

		source.clip = clips[randomClip];

		//source.outputAudioMixerGroup = output;

		source.pitch = Random.Range (minPitch, maxPitch);

		source.Play ();
		
		Destroy(source, clips[randomClip].length);
	
	}
}
