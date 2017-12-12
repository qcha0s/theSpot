using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Heal : MonoBehaviour {
	private BS_PlayerHealth playerHealth; 
void OnTriggerEnter(Collider other){
		
		Debug.Log("heal");
			playerHealth = other.GetComponent<BS_PlayerHealth>();
		if(playerHealth == null){
			return;


		}else{
			
			Debug.Log(playerHealth == null);
			playerHealth.PlayerHeal(100);
			Destroy(gameObject);
		}
}
}
