using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedMage : NetworkBehaviour {

	public BS_Mage mage;


	private NetworkAnimator m_networkAnimator;
	private Animator m_animator;
	//private bool isAttacking = false;
	//private bool isPoly = false;
	//private bool isBlinking = false;

	void Start() {
		m_networkAnimator = GetComponent<NetworkAnimator>();
		m_animator = GetComponent<Animator>();
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)) {
			NetworkSyncTrigger("isAttacking");
			//isAttacking = true;
		}
		if(Input.GetKeyDown(KeyCode.Alpha1) && !mage.m_PolyonCD) {
			NetworkSyncTrigger("isPoly");
			//isPoly = true;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && !mage.m_BlinkOnCD) {
			NetworkSyncTrigger("isBlinking");
			//isBlinking = true;
		}
	}

	void NetworkSyncTrigger(string triggerName) {
        m_networkAnimator.SetTrigger(triggerName);

        if (NetworkServer.active)
            m_animator.ResetTrigger(triggerName);
    }

	[Command]
	void CmdFire() {
		
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (mage.fireBallPrefab, mage.spellSpawn.position, mage.spellSpawn.rotation);

		NetworkServer.Spawn(bullet);
			
		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
		
	}

	public void Fire() {
		if(isLocalPlayer){
			CmdFire();
		}
	}

 	void Blink() {
		if(isLocalPlayer) {
			transform.position += transform.rotation * Vector3.forward * 5;
			mage.m_BlinkOnCD = true;
			mage.m_CDMasks[1].fillAmount = 1;
			StartCoroutine(mage.CoolDownSystem(mage.m_BlinkCD,"Blink"));
		}
	}

	[Command]
 	void CmdPolymorph() {
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (mage.polyPrefab, mage.spellSpawn.position, mage.spellSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
		mage.m_PolyonCD = true;
		mage.m_CDMasks[0].fillAmount = 1;
		StartCoroutine(mage.CoolDownSystem(mage.m_PolyCD,"Poly"));
		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

	public void Polymorph() {
		if(isLocalPlayer) {
			CmdPolymorph();
		}
	}

	// 	public void Ultimate() {
	//    	 	// Create the Bullet from the Bullet Prefab
	// 		var bullet = (GameObject)Instantiate (ultimatePrefab, ultPos.position, ultPos.rotation);

	// 		// Add velocity to the bullet
	// 		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
	// 	}
}
