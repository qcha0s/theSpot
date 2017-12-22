using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWeaponScript : MonoBehaviour {
   public bool m_poisoned = false;
   private float m_weapDmg = 20;

   private float m_poisonDmg=5;
   private float m_poisonCount=5;

   private float m_totalPoisonTime=2;
    private RPGCharacterController m_rpgController;
	private BS_Rogue m_RogueController;


    void Start(){
        m_rpgController = transform.root.GetComponent<RPGCharacterController>();
        m_RogueController = transform.root.GetComponent<BS_Rogue>();
    }
	void OnTriggerEnter(Collider other){
		if(other.tag == "target" && !m_rpgController.m_hasDealtDamage){
			 Vector3 toTarget = (other.transform.position - transform.position).normalized;
             
            if(m_poisoned){
                other.GetComponent<BS_Health>().TakeDotDamage(m_poisonDmg,m_poisonCount,m_totalPoisonTime);
            }
             if (Vector3.Dot(toTarget, other.transform.forward) > 0) {
                 other.GetComponent<BS_Health>().TakeDamage(m_weapDmg*2);
             } else {
                 other.GetComponent<BS_Health>().TakeDamage(m_weapDmg);
             }
		}
        m_rpgController.m_hasDealtDamage = true;

	}
    public void SetPoison(bool ispoisoned){
        m_poisoned = ispoisoned;
        
    }
}
