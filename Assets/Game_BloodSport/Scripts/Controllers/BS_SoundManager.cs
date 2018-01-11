using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_SoundManager : MonoBehaviour {

	public AudioSource m_audioSource;
	public GameObject m_playerPrefab;
	public AudioClip mage_BH;
	public AudioClip mage_Fire;
	public AudioClip mage_Poly;
	public AudioClip mage_Blink;

	public AudioClip warrior_ChargeHit;
	public AudioClip warrior_ArmorHit;
	public AudioClip warrior_Whirlwind;
	public AudioClip warrior_Sword;
	public AudioClip warrior_ChargeCry;

	public AudioClip rogue_BackStab;
	public AudioClip rogue_Poison;
	public AudioClip rogue_SmokeBomb;
	public AudioClip rogue_Dagger;

	public AudioClip archer_ArrowHit;
	public AudioClip archer_ArrowShot;
	public AudioClip archer_Disengage;
	public AudioClip archer_IceImpact;

	public AudioClip general_Jump;
	public AudioClip general_BladeSwing;

	void Start(){
		//m_audioSource = m_playerPrefab.
	}
	public void PlayBlackhole(){
		m_audioSource.PlayOneShot(mage_BH);
	}
	public void PlayBlink(){
		m_audioSource.PlayOneShot(mage_Blink);
	}
	public void PlayFire(){
		m_audioSource.PlayOneShot(mage_Fire);
	}
	public void PlayPoly(){
		m_audioSource.PlayOneShot(mage_Poly);
	}

	public void PlayCharge(){
		m_audioSource.PlayOneShot(warrior_ChargeHit);
	}
	public void PlayWhirlwind(){
		m_audioSource.PlayOneShot(warrior_Whirlwind);
	}
	public void PlayArmorStrike(){
		m_audioSource.PlayOneShot(warrior_ArmorHit);
	}
	public void PlayChargeCry(){
		m_audioSource.PlayOneShot(warrior_ChargeCry);
	}



	public void PlayPoison(){
		m_audioSource.PlayOneShot(rogue_Poison);
	}
	
	public void PlaySmoke(){
		m_audioSource.PlayOneShot(rogue_SmokeBomb);
	}
	public void PlayBackStab(){
		m_audioSource.PlayOneShot(rogue_BackStab);
	}
	public void PlayDaggerSwipe(){
		m_audioSource.PlayOneShot(rogue_Dagger);
	}


	public void PlayArrowHit(){
		m_audioSource.PlayOneShot(archer_ArrowHit);
	}
	public void PlayArrowShot(){
		m_audioSource.PlayOneShot(archer_ArrowShot);
	}
	public void PlayDisengage(){
		m_audioSource.PlayOneShot(archer_Disengage);
	}
	public void PlayIceImpact(){
		m_audioSource.PlayOneShot(archer_IceImpact);
	}
	public void PlayJump(){
		m_audioSource.PlayOneShot(general_Jump);
	}
	public void PlaySwordSwing(){
		m_audioSource.PlayOneShot(general_BladeSwing);
	}

}
