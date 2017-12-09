using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BattleScript : NetworkBehaviour {

	public Transform prefab;
	public GameObject prefabSpawnPoint;
	public float m_bulletSpace = 0.5f;
	public float m_fireBallSpeed = 30.0f;
	private bool m_isAttacking = false;
	private NetworkAnimator m_networkAnimator;
    private Animator m_animator;
    void Start() {
		if(isLocalPlayer){
			gameObject.layer = LayerMask.NameToLayer("Player");
		}
		else{
			gameObject.layer = LayerMask.NameToLayer("Enemy");
		}
		m_animator = GetComponent<Animator>();
		m_networkAnimator = GetComponent<NetworkAnimator>();
	}

	void Update() {
        if (isLocalPlayer)
        {
            if(Input.GetButton("Attack") && !m_isAttacking) {
                NetworkSyncTrigger("isAttacking");
                m_isAttacking = true;
            }
        }
    }
    void NetworkSyncTrigger(string triggerName)
    {
        m_networkAnimator.SetTrigger(triggerName);

        if (NetworkServer.active)
            m_animator.ResetTrigger(triggerName);
    }
	[Command]
	void CmdAttack(Quaternion bulletRotation){
		GameObject fireBall = Instantiate(prefab, prefabSpawnPoint.transform.position + transform.forward * m_bulletSpace, bulletRotation).gameObject;
        fireBall.GetComponent<Rigidbody>().velocity = fireBall.transform.forward * m_fireBallSpeed;
		//fireBall.GetComponent<Bullet>().SetOwner(gameObject);
        NetworkServer.Spawn(fireBall);
        Destroy(fireBall.gameObject, 2.0f);
	}
    public void CmdSpawnAttack() {
		if(isLocalPlayer){
            Quaternion bulletRotation = AimAtCrosshair();
            CmdAttack(bulletRotation);
        }
	}

	public void CmdEndAttack(){
		if(isLocalPlayer){
            m_isAttacking = false;
        }
	}

	Quaternion AimAtCrosshair(){
		Camera thirdPersonCamera = Camera.main;
        Quaternion retVal = thirdPersonCamera.transform.rotation;
        RaycastHit hitInfo;
		Ray aimRay = thirdPersonCamera.ScreenPointToRay(new Vector3(thirdPersonCamera.pixelWidth * 0.5f, thirdPersonCamera.pixelHeight * 0.5f, 0));

		bool didHit = Physics.Raycast(
			aimRay.origin, 
			aimRay.direction, 
			out hitInfo, 
			Mathf.Infinity, 
			LayerMask.NameToLayer("Ground") | LayerMask.NameToLayer("Enemy"));
		if(didHit){
			retVal = Quaternion.LookRotation(hitInfo.point - prefabSpawnPoint.transform.position);
		}
		return retVal;
	}
}
