using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_UD : MonoBehaviour {

	public List<GameObject> m_armors;
	public List<GameObject> m_weapons;

	private void Awake() {
		InitArmor();
		InitWeapons();
	} 

	private void InitArmor() {
		int armorAmt = Random.Range(0,m_armors.Count);
		while (armorAmt > 0) {
			GameObject tempArmor = m_armors[Random.Range(0,m_armors.Count)];
			tempArmor.SetActive(true);
			m_armors.Remove(tempArmor);
			armorAmt--;
		}
	}

	private void InitWeapons() {
		m_weapons[Random.Range(0,m_weapons.Count)].SetActive(true);
	}
}
