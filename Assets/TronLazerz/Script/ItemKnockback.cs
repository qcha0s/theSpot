using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKnockback : MonoBehaviour {

 // public Rigidbody rigidbody;


    void OnCollisionEnter(Collision other){
     
        if(other.gameObject.tag == "paddle"){
          Debug.Log("we get here");
          Vector3 m_force = transform.position - other.transform.position;
          gameObject.GetComponent<Rigidbody>().AddForce(m_force, ForceMode.Impulse);
        }
    }

   /* public float maxSpeed = 1f;//Replace with your max speed
        void FixedUpdate(){
              if(rigidbody.velocity.magnitude > maxSpeed)
              {
                      rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
              }
        }*/
}