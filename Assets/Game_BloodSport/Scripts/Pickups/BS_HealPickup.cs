using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_HealPickup : MonoBehaviour {
	private BS_Health playerHealth; 
void OnTriggerEnter(Collider other){
		
		//Debug.Log("heal");
		playerHealth = other.GetComponent<BS_Health>();
		if(playerHealth == null){
			return;


		}else{
			playerHealth.Heal(100);
			Destroy(gameObject);
		}
}
}
