using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_CapSpawner : MonoBehaviour {

	public Transform HealthPoint;
	public Transform UltPoint;
	public Transform PowerPoint;
	public GameObject HealthPre;
	public GameObject UltPre;
	public GameObject PowerPre; 
	public int capTimer = 0;
	public int currentTime;
	public int refValue = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine(CapTimer());
		
	}
	IEnumerator CapTimer(){
	yield return new WaitForSeconds(30);
	CapSpawner();
	}

	public void CapSpawner(){
		++refValue;
		if (refValue == 1){
			GameObject HealthCapsule = (GameObject)Instantiate(HealthPre, HealthPoint.position, HealthPoint.rotation);
			StartCoroutine(CapTimer());
		}
		if (refValue == 3){
			GameObject UltCapsule = (GameObject)Instantiate(UltPre, UltPoint.position, UltPoint.rotation);
			
		}
		if (refValue == 2){
			GameObject PowerCapsule = (GameObject)Instantiate(PowerPre, PowerPoint.position, PowerPoint.rotation);
			StartCoroutine(CapTimer());
		}
		
		
	}
	
	
}
