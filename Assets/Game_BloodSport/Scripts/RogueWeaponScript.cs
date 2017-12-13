using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWeaponScript : MonoBehaviour {
   public bool m_poisoned;
   private float m_weapDmg = 20;

   private float m_poisonDmg=5;
   private float m_poisonCount=5;

   private float m_totalPoisonTime=2;
	void OnTriggerEnter(Collider other){
		if(other.tag == "target"){
			 Vector3 toTarget = (other.transform.position - transform.position).normalized;
             
            if(m_poisoned){
                other.GetComponent<BS_Health>().TakeDotDamage(m_poisonDmg,m_poisonCount,m_totalPoisonTime);
            }

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
        
    }
}
