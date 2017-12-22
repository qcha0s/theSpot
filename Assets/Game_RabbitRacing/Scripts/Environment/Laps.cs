using UnityEngine;
using System.Collections;
 
public class Laps : MonoBehaviour {
     
	// These Static Variables are accessed in "checkpoint" Script
	public Transform[] m_checkPointArray;
	public static Transform[] m_checkpointA;
	public static int m_currentCheckpoint = 0; 
	public static int m_currentLap = 0; 
	public Vector3 m_startPos;
	public int m_lap;
     
	void  Start ()
	{
		m_startPos = transform.position;
		m_currentCheckpoint = 0;
		m_currentLap = 0; 
 
	}
 
	void  Update ()
	{
		m_lap = m_currentLap;
		m_checkpointA = m_checkPointArray;
	}
     
}