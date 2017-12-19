using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_UD : Singleton<SoundManager_UD> {
	
	
	//Make all the clips static
	public static AudioClip PlayerHit, CannonExplosion, PlayerAttack, EnemyHit1, EnemyHit2, EnemyHit3, EnemyDie;
	static AudioSource m_audioSource;

	private void Start() {
		m_audioSource = GetComponent<AudioSource>();
		PlayerHit = Resources.Load<AudioClip>("PlayerHit");
		CannonExplosion = Resources.Load<AudioClip>("CannonExplosion");
		PlayerAttack = Resources.Load<AudioClip>("PlayerAttack");
		EnemyHit1 = Resources.Load<AudioClip>("EnemyHit1");
		EnemyHit2 = Resources.Load<AudioClip>("EnemyHit2");
		EnemyHit3 = Resources.Load<AudioClip>("EnemyHit3");
		EnemyDie = Resources.Load<AudioClip>("EnemyDie");
	}
	public static void PlaySound (string clip) {
		switch (clip) {
				case "PlayerHit":
					m_audioSource.PlayOneShot(PlayerHit);
					break;
				case "Cannon Explosion":
					m_audioSource.PlayOneShot(CannonExplosion);
					break;
				case "PlayerAttack":
					m_audioSource.PlayOneShot(PlayerAttack);
					break;
				case "EnemyHit1":
					m_audioSource.PlayOneShot(EnemyHit1);
					break;
				case "EnemyHit2":
					m_audioSource.PlayOneShot(EnemyHit2);
					break;
				case "EnemyHit3":
					m_audioSource.PlayOneShot(EnemyHit3);
					break;
				case "EnemyDie":
					m_audioSource.PlayOneShot(EnemyDie);
					break;
				}
		}
	}