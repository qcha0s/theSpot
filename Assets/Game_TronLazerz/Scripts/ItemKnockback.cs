using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKnockback : MonoBehaviour {

    private bool m_hit = false;

    public bool IsHit(){
        return m_hit;
    }

    public void Hit(bool hit){
        m_hit = hit;
    }


 
    void OnCollisionStay(Collision other)
    {
         
            if(other.gameObject.tag =="paddle"){

          // calculate force vector
            Vector3 m_force = transform.position - other.transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(m_force, ForceMode.Impulse);

           
        }
     
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag =="paddle" || other.gameObject.tag =="Interactable" ){
            m_hit = true;
            
        }
    }


}