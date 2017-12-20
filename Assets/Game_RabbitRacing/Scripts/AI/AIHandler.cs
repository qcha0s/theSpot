using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent( typeof( BaseKartMovement ) )]
public class AIHandler : MonoBehaviour {
	private Vector3 m_Destination;
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
			Vector3 localDestination = transform.InverseTransformPoint(m_Destination);
			Vector2 localDestinationPlane = new Vector2(localDestination.x, localDestination.z);
			Debug.Log(localDestination);
			if(localDestinationPlane.y > 0){
				float distance = localDestinationPlane.magnitude;
				float sinOfAngle = localDestinationPlane.x / distance;
				float desiredTurnRadius = distance * 0.5f / sinOfAngle;
				float turnAmount = m_baseKartMovement.GetTurnAmountForTurnRadius(desiredTurnRadius);
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
}
