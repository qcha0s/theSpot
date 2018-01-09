using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class WayPoint : MonoBehaviour {
	public WayPoint[] m_nextWayPoints;
	private int m_wayPointNumber;
	private static Color[] m_gizmoColours = {Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta};
	public int WayPointNumber{
		get{
			return m_wayPointNumber;
		}
		set{
			m_wayPointNumber = value;
		}
	}
	public WayPoint NextWayPoint{
		get{
			WayPoint retVal = null;
			if(m_nextWayPoints.Length > 0){
				retVal = m_nextWayPoints[Random.Range(0, m_nextWayPoints.Length)];
			}
			return retVal;
		}
	}
	public Vector3 Point{
		get{
			return transform.position;
		}
	}
	private void OnTriggerEnter(Collider other) {
		AIHandler kartBot = other.GetComponent<AIHandler>();
		if(kartBot != null){
			kartBot.Destination = NextWayPoint.Point;
		}
	}
	private void OnDrawGizmos() {
		Gizmos.color = m_gizmoColours[m_wayPointNumber % m_gizmoColours.Length];
		Gizmos.DrawSphere(transform.position, 1f);
		foreach (WayPoint nextWayPoint in m_nextWayPoints){
			if(nextWayPoint != null){
				Vector3 nextWayPointPosition = nextWayPoint.transform.position;
				Gizmos.DrawLine(transform.position, nextWayPointPosition);
			}
		}
	}
}
