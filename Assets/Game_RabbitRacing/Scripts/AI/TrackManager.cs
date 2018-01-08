using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrackManager : MonoBehaviour {
	public int m_startLineNumber = 0;
	public WayPoint m_startLine;
	// Use this for initialization
	void Awake () {
		NumberWayPoints();
	}
	public void InitialSetupWayPoints(){
		for(int i = 0; i < transform.childCount; i++){
			WayPoint currentWayPoint = transform.GetChild(i).GetComponent<WayPoint>();
			WayPoint nextWayPoint = transform.GetChild((i + 1) % transform.childCount).GetComponent<WayPoint>();
			currentWayPoint.m_nextWayPoints = new WayPoint[] {nextWayPoint};
		}
	}
	public void NumberWayPoints(){
		RecursiveNumberWayPoints(m_startLine, m_startLineNumber);
	}
	void RecursiveNumberWayPoints(WayPoint root, int wayPointNumber){
		if(root == m_startLine && wayPointNumber != m_startLineNumber){
			return;
		}
		root.WayPointNumber = wayPointNumber;
		foreach(WayPoint wayPoint in root.m_nextWayPoints){
			RecursiveNumberWayPoints(wayPoint, wayPointNumber+1);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
