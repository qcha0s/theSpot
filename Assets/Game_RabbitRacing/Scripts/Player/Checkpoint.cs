using UnityEngine;
using System.Collections;
 
public class Checkpoint : MonoBehaviour {
     
	void  Start ()
	{
 
	}
     
	void  OnTriggerEnter ( Collider other  )
	{
		//Is it the Player who enters the collider?
		if (!other.CompareTag("Player")) 
			return; //If it's not the player dont continue
         
 
		if (transform == Laps.m_checkpointA[Laps.m_currentCheckpoint].transform) 
		{
			//Check so we dont exceed our checkpoint quantity
			if (Laps.m_currentCheckpoint + 1 < Laps.m_checkpointA.Length) 
			{
				//Add to currentLap if currentCheckpoint is 0
				if(Laps.m_currentCheckpoint == 0)
					Laps.m_currentLap++;
				Laps.m_currentCheckpoint++;
			} 
			else 
			{
				//If we dont have any Checkpoints left, go back to 0
				Laps.m_currentCheckpoint = 0;
			}
		}
 
 
	}
 
}