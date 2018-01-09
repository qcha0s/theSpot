using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyCollider : MonoBehaviour {
	public GameObject m_player;
	public GameObject m_sheep;
	public bool m_isMage = false;
	public float m_polyDuration = 7.0f;

	private bool isPoly = false;

	void Start () {
		m_player.SetActive(true);
		m_sheep.SetActive(false);
	}
	
	void OnTriggerEnter(Collider other) {
		if(!m_isMage) {
			if(!isPoly) {
				if(other.gameObject.name == "Polymorph(Clone)") {
					isPoly = true;
					SetActives(false, true);
					StartCoroutine(PolyDuration(m_polyDuration));
				}
			} else {
				if(!(other.gameObject.name == "Polymorph(Clone)")) {
					CancelPoly();
				}
			}
		}
	}

	IEnumerator PolyDuration(float cooldownvalue){	
		yield return new WaitForSeconds(cooldownvalue);
		CancelPoly();
	}

	private void CancelPoly() {
		isPoly = false;
		SetActives(true, false);
	}

	private void SetActives(bool player, bool sheep) {
		m_player.SetActive(player);
		m_sheep.SetActive(sheep);
	}
}
