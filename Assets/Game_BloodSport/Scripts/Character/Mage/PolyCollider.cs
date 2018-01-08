using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyCollider : MonoBehaviour {
	public GameObject m_player;
	public GameObject m_sheep;
	public bool m_isMage = false;

	private bool isPoly = false;

	void Start () {
		m_player.SetActive(true);
		m_sheep.SetActive(false);
	}
	
	void OnCollisionEnter(Collision other) {
		if(!m_isMage) {
			if(!isPoly) {
				if(other.gameObject.name == "Polymorph(Clone)") {
					isPoly = true;
					m_player.SetActive(false);
					m_sheep.SetActive(true);
				}
			} else {
				isPoly = false;
				m_player.SetActive(true);
				m_sheep.SetActive(false);
			}
		}
	}
}
