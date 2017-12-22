using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHockeyAI : MonoBehaviour {

     Transform bar;
  
     void Start() {
       
     }

		 
  
     void Update() { 
		bar = GameObject.FindWithTag("Player").transform;
        transform.position = new Vector3(bar.position.x, transform.position.y, transform.position.z);
     }
 }

