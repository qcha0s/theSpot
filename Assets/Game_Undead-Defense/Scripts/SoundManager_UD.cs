using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_UD : MonoBehaviour {

    public enum SoundTypes {Shoot, travel ,impact}
    private AudioSource m_audio;
    public AudioClip[] m_Sounds;

    private void Start() {
        m_audio = GetComponent<AudioSource>();
    }

    public void PlayShootSound(){
        m_audio.clip = m_Sounds[0];
        m_audio.Play();
    }

    public void PlayExplosionSound() {
        m_audio.clip = m_Sounds[2];
        m_audio.Play();
    }

    public void PlaySound(int type) {
        m_audio.clip = m_Sounds[type];
        m_audio.Play();
    }
}