using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLines : MonoBehaviour {
	
	public List<Vector3> positions = new List<Vector3>();
	public float interval;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(newUpdate());
	}

	protected IEnumerator newUpdate () {
		
		yield return new WaitForSeconds(interval);
		
		if (EZReplayManager.get.getCurrentMode() == ViewMode.LIVE && EZReplayManager.get.getCurrentAction() == ActionMode.RECORD) {
			
			positions.Add (transform.position);	
			
		}
		
		StartCoroutine(newUpdate());
	}
	
	//EZReplayManager callback for start of replay-mode:
	public void __EZR_replay_ready() {
		
		if (positions.Count < 1) {
			EZR_Clone cloneScript = GetComponent<EZR_Clone>(); 
			
			GameObject go = null;
			if (cloneScript.origInstanceID > -1)
				go = EZReplayManager.get.instanceIDtoGO[cloneScript.origInstanceID];
			
			if(go != null)
				positions = go.GetComponent<DrawLines>().positions;
		}
	}
	
	void Update() {
 		if (EZReplayManager.get.getCurrentMode() == ViewMode.REPLAY) {
			
			Vector3 oldPos = Vector3.zero;
			
			foreach(Vector3 pos in positions) {
				
				if (oldPos != Vector3.zero) {
					
					Debug.DrawLine(oldPos, pos, Color.red);
				}
				
				oldPos = pos;
				
			}
			
		}		
	}
}
