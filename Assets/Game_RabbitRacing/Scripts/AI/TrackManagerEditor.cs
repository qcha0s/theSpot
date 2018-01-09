using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TrackManager))]
public class TrackManagerEditor : Editor {
	public override void OnInspectorGUI()
   {
       DrawDefaultInspector();
        
       TrackManager trackManager = (TrackManager)target;
       if(GUILayout.Button("InitializeWayPoints"))
       {
           trackManager.InitialSetupWayPoints();
       }
		if(GUILayout.Button("NumberWayPoints")){
			trackManager.NumberWayPoints();
		}
   }
}
