using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedNetScore : MonoBehaviour {

	public ScoreManager m_sm;

	void OnTriggerEnter(Collider other){
		if(gameObject.tag == "Player"){
			Destroy(gameObject);
			m_sm.scored_Blue = true;
		}
	}
}
