using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedNetScore : MonoBehaviour {

	public ScoreManager m_sm;

	void Awake(){
			m_sm = GetComponent<ScoreManager>();
		}
		
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "goal"){
			Destroy(other.gameObject);
			m_sm.scored_Blue = true;
			m_sm.m_goal = true;
		}
	}
}
