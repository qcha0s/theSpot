using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueNetScore : MonoBehaviour {
	
public ScoreManager m_sm;

	void Awake(){
		m_sm = GetComponentInParent<ScoreManager>();
		Debug.Log(m_sm == null);
	}	

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "goal"){
			Destroy(other.gameObject);
			m_sm.scored_Red = true;
			m_sm.m_goal = true;
		}
	}
}
