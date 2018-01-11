using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ItemKnockback : NetworkBehaviour {


    void OnCollisionStay(Collision other)
    {
         
            if(other.gameObject.tag =="paddle"){

          // calculate force vector
            Vector3 m_force = transform.position - other.transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(m_force, ForceMode.Impulse);
         }
     
    }


}