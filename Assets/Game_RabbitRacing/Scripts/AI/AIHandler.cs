using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent( typeof( BaseKartMovement ) )]
public class AIHandler : MonoBehaviour {
	public Vector3 m_Destination;
	private BaseKartMovement m_baseKartMovement;
	private void Awake() {
		m_baseKartMovement = GetComponent<BaseKartMovement>();
	}
	public Vector3 Destination{
		get{
			return m_Destination;
		}
		set{
			m_Destination = value;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(m_Destination != null){
			Vector2 localPlaneDestination = ToLocalPlane(m_Destination);
			if(localPlaneDestination.y > 0){
				float desiredTurnRadius = CalculateTurnDirectionToDestination(localPlaneDestination);
				float turnAmount = m_baseKartMovement.GetTurnAmountForTurnRadius(desiredTurnRadius);
				Debug.Log("Radius: " + desiredTurnRadius);
				Debug.Log("Turn: " + turnAmount);
				if(Mathf.Abs(turnAmount) > 1){
					m_baseKartMovement.Brake(1);
				}
				else{
					m_baseKartMovement.Gas(1);
					m_baseKartMovement.Turn(turnAmount);
				}
			}
			else{
				m_baseKartMovement.Brake(1);
			}
		}
	}
	float CalculateTurnDirectionToDestination(Vector2 localPlaneDestination){
		float distance = localPlaneDestination.magnitude;
		float sinOfAngle = localPlaneDestination.x / distance;
		float desiredTurnRadius = distance * 0.5f / sinOfAngle;
		return desiredTurnRadius;
	}
	Vector2 ToLocalPlane(Vector3 destination){
		Vector3 localDestination = transform.InverseTransformPoint(destination);
		Vector2 localDestinationPlane = new Vector2(localDestination.x, localDestination.z);
		return localDestinationPlane;
	}
}
