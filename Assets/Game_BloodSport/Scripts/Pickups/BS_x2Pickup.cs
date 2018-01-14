using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_x2Pickup : MonoBehaviour {

	private int multiplyTimer = 4;
    private RPGCharacterController m_playerscript;
	
      void Update(){
	
	}
    void OnTriggerEnter (Collider other)
    {
		m_playerscript = other.GetComponent<RPGCharacterController>();
		if(m_playerscript == null){
			return;
		}else{
			m_playerscript.Multiply(multiplyTimer);
			Destroy(gameObject);
		}
		
		
      
	

	}
	
}

		
    

	
	

	
	// Update is called once per frame
	

