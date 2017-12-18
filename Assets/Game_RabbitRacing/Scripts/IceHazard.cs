using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class IceHazard : MonoBehaviour {

    [SerializeField]private float m_timerLength = 5f;
    [SerializeField]private float m_timerTimePassed = 0f;
    [SerializeField]private bool m_runTimer = false;
    private BaseKartMovement m_baseKartMovement;


    void  OnDrawGizmos() {
        //TODO remove after testing
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 1);
    }

    void Awake() {
        m_baseKartMovement = GetComponent<BaseKartMovement>();
    }


    void OnTriggerEnter(Collider other) {
               if(other.tag == "Player") {
                   m_runTimer = true;
                  //
            Debug.Log("Hit");
        }
    }
    void Update() {
        if (m_runTimer) {
            m_timerTimePassed += Time.deltaTime;

            if (m_timerTimePassed >= m_timerLength) {
                m_timerTimePassed = 0f;
                m_runTimer = false;
                //m_baseKartMovement.m_physicMaterial.dynamicFriction = 1;
                //m_baseKartMovement.GetComponent<Collider>().material.dynamicFriction = 1;
                Destroy(gameObject);
            }
        }
  }

}
