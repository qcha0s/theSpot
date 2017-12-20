using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKnockback : MonoBehaviour {


    void OnCollisionStay(Collision other)
    {
         
            if(other.gameObject.tag =="paddle"){

          // calculate force vector
            Vector3 m_force = transform.position - other.transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(m_force, ForceMode.Impulse);

            FindObjectOfType<AudioManager>().Play("PuckHit");
        }
     
    }


}