using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour {
	private Vector2 m_target;
	public Vector2 Target{
		get{
			return m_target;
		}
		set{
			m_target = value;
		}
	}
	private BaseKartMovement m_kartMovement;
	void Awake() {
		m_kartMovement = GetComponent<BaseKartMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		float desiredTurnRadius = CalculateTurnRadius();
	}
	float CalculateTurnRadius(){
		float retVal = 0;
		
		return retVal;
	}
}
