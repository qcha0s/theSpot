using UnityEngine;
using System.Collections;

public class ChangeCam : MonoBehaviour {
	
	void Awake() {
		if (gameObject.name == "LiveCamera") {
			GameObject goReplayCAM = GameObject.Find ("ReplayCamera");
			goReplayCAM.GetComponent<Camera>().enabled = false;
			GetComponent<Camera>().enabled = true;
		}		
	}
	
	public void __EZR_live_ready() {
		
		if (gameObject.name == "LiveCamera") {
			GameObject goReplayCAM = GameObject.Find ("ReplayCamera");
			goReplayCAM.GetComponent<Camera>().enabled = false;
			GetComponent<Camera>().enabled = true;
		}
		
	}
	
	public void __EZR_replay_ready() {
		
		if (gameObject.name == "ReplayCamera") {
			GameObject goReplayCAM = GameObject.Find ("LiveCamera");
			goReplayCAM.GetComponent<Camera>().enabled = false;
			GetComponent<Camera>().enabled = true;
		}
		
	}	
}
