using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TronPlayerMovement : NetworkBehaviour {

	float distance = 10;

    

    
    public Rigidbody rb_paddle;
        void Start() {
            rb_paddle = GetComponent<Rigidbody>();
            
        }

    
        void Update(){
            if(isLocalPlayer){
                if (Input.GetMouseButton(0)) {
                    Cursor.visible = false;
                    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                    Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    transform.position = objPosition;
                
                }else{
                    Cursor.visible = true;
                }
            }
        }
   
}