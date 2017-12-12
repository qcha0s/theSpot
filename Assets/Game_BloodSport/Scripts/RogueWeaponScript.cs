using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWeaponScript : MonoBehaviour {
    bool m_poisoned;
    float m_weapDmg = 20;

    float m_poisonDmg;
	void OnTriggerEnter(Collider other){
		if(other.tag == "target"){
			 Vector3 toTarget = (other.transform.position - transform.position).normalized;
             
             if (Vector3.Dot(toTarget, other.transform.forward) > 0) {
                 Debug.Log("Player Behind Object.");
                 other.GetComponent<BS_Health>().TakeDamage(m_weapDmg*2);
             } else {
                 Debug.Log("Player in front Object.");
                 other.GetComponent<BS_Health>().TakeDamage(m_weapDmg);
             }
		}

	}

public void SetPoison(bool ispoisoned){
        m_poisoned = ispoisoned;
        Debug.Log("We Poisoned Daggers");
    }
}
