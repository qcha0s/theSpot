using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWeaponScript : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if(other.tag == "target"){
			 Vector3 toTarget = (other.transform.position - transform.position).normalized;
             
             if (Vector3.Dot(toTarget, other.transform.forward) > 0) {
                 Debug.Log("Player Behind Object.");
             } else {
                 Debug.Log("Player in front Object.");
             }
		}

	}
}
