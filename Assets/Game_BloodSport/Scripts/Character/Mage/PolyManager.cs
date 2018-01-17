using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyManager : MonoBehaviour {

	public GameObject m_player;
	public GameObject m_sheep;
	public float m_polyDuration = 7.0f;

	private bool m_isPolyd = false;

	void Start() {
		SetActives(true, false);
	}

	public void SetPoly() {
		if(!m_isPolyd) {
			m_isPolyd = true;
			m_sheep.transform.position = m_player.transform.position;
			SetActives(false, true);
			StartCoroutine(PolyDuration(m_polyDuration));
		}
	}

	public void CancelPoly() {
		SetActives(true, false);
		m_isPolyd = false;
	}

	private void SetActives(bool player, bool sheep) {
		m_player.SetActive(player);
		m_sheep.SetActive(sheep);
	}

	IEnumerator PolyDuration(float cooldownvalue){	
		yield return new WaitForSeconds(cooldownvalue);
		CancelPoly();
	}
}