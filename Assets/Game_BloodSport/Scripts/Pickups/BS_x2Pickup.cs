using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_x2Pickup : MonoBehaviour {

	private int multiplyTimer = 0;
    private BS_testmove activeMultiplier;
	
      void Update(){
	
	}
    void OnTriggerEnter (Collider other)
    {
		activeMultiplier = other.GetComponent<BS_testmove>();
		if(activeMultiplier == null){
			return;
		}else{
		activeMultiplier.Multiply();
		Destroy(gameObject);
		}
		
		
      
	

	}
	
}

		
    

	
	

	
	// Update is called once per frame
	

