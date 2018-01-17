using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour {

	private WayPoint m_currentWaypoint;
	private int m_lapNumber = 0;
	private float m_distanceToNextWaypoint;
	public WayPoint CurrentWayPoint{
		get{
			return m_currentWaypoint;
		}
		set{
			if(m_currentWaypoint){
				if(m_currentWaypoint.HasNextPoint(value)){
					m_currentWaypoint = value;
					if(value.WayPointNumber == 0){
						IncrementLapNumber();
					}
				}
			}
			else if(value.WayPointNumber == 0){
				m_currentWaypoint = value;
			}
		}
	}
	public void IncrementLapNumber(){
		m_lapNumber++;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(m_currentWaypoint){
			float minDistance = float.MaxValue;
			WayPoint[] nextWaypoints = m_currentWaypoint.NextWayPoints;
			foreach(WayPoint wayPoint in nextWaypoints){
				float distance = GetDistance(wayPoint);
				minDistance = Mathf.Min(distance, minDistance);
			}
			m_distanceToNextWaypoint = minDistance;
			Debug.Log(m_currentWaypoint.WayPointNumber + ", " + m_distanceToNextWaypoint + ", " + m_lapNumber);
		}
	}
	float GetDistance(WayPoint wayPoint){
		return (wayPoint.transform.position - transform.position).magnitude;
	}
}
