using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastInteraction_UD : MonoBehaviour {

	public enum GameMode {TheSpot, Undead_Defense}
	public float m_raycastDistance = 20f;
	private InteractableObject_UD m_object;
	public GameMode m_gameMode = GameMode.Undead_Defense;

	private void Update() {
		RayCastForBlock();
		if (Input.GetAxis("Interact") > 0) {
			if (m_object != null) {	
				if (m_gameMode == GameMode.Undead_Defense) {
					GameManager_UD.instance.SetPlayerMovementScript(GetComponent<CharacterMovement_UD>());
				}
				m_object.Interact();
			}
		}
	}

	void RayCastForBlock() {
		RaycastHit hit;
		Vector3 startPos = Camera.main.transform.position;
		Vector3 forward = Camera.main.transform.forward * m_raycastDistance;
		Debug.DrawRay(startPos, forward, Color.blue);
		if (Physics.Raycast(startPos,forward, out hit,m_raycastDistance)) {
			if (hit.collider.tag == "Interactable") {
				InteractableObject_UD newObj = hit.collider.GetComponent<InteractableObject_UD>();
				if (m_object == null) {
					m_object = newObj;
					m_object.OnBeginRaycast();
				} else if (newObj != m_object) {
					m_object.OnEndRaycast();
					m_object = newObj;
					m_object.OnBeginRaycast();
				}
			} 
			// else if (m_object != null) {
			//  	NormalizeObject();
			// 	 Debug.Log("here");
			// }
		} else if (m_object != null) {
			NormalizeObject();
		}
	}

	private void NormalizeObject() {
		m_object.OnEndRaycast();
		m_object = null;
	}
}
