using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class IceHazard : MonoBehaviour {

    [SerializeField]private float m_timerLength = 5f;
    [SerializeField]private float m_timerTimePassed = 0f;
    [SerializeField]private bool m_runTimer = false;
    private CharacterControllerKart m_KartController;
    private InputHandler m_inputHandler;
    private float m_thisMinTurnRadius;
    private float m_thisMaxTurnRadius;


    

    void Awake() {
        m_KartController = GetComponent<CharacterControllerKart>();
        m_inputHandler = GetComponent<InputHandler>();
    }


    void OnTriggerEnter(Collider other) {
               if(other.tag == "Player") {
                   other.gameObject.GetComponent<InputHandler>().enabled = false;
                   m_runTimer = true;
                   m_inputHandler.enabled = false;
                  
            Debug.Log("Hit");
        }
    }
    void Update() {
        if (m_runTimer) {
            m_timerTimePassed += Time.deltaTime;

            if (m_timerTimePassed >= m_timerLength) {
                m_timerTimePassed = 0f;
                m_runTimer = false;
                m_inputHandler.enabled = true;
            }
        }
  }

}
