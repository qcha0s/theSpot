using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    [SerializeField] private float m_timerLength = 5f;
    [SerializeField] private float m_timerTimePassed = 0f;
    [SerializeField] private bool m_runTimer = false;
    private CharacterControllerKart m_kartController;
    private InputHandler m_inputHandler;
    private PlayerStatus m_playerStatus;
    [SerializeField] private bool m_isInvincible;
    [SerializeField] private bool m_isRoadRage;
    [SerializeField] private bool m_isIce;
    [SerializeField] private bool m_isDisabled;



    public bool IsRoadRage {
        get { return m_isRoadRage; }
        set { m_isRoadRage = value; }
    }
    public bool IsInvincible
    {
        get { return m_isInvincible; }
        set { m_isInvincible = value; }
    }
    public bool IsDisabled
    {
        get { return m_isDisabled; }
        set { m_isDisabled = value; }
    }

    void Awake() {
        m_kartController = GetComponent<CharacterControllerKart>();
        m_inputHandler = GetComponent<InputHandler>();
        m_playerStatus = GetComponent<PlayerStatus>();
    }


   void OnTriggerEnter(Collider other) {
       if (other.CompareTag("Ice")) {
           m_isIce = true;
         m_runTimer = true;
         m_inputHandler.enabled = false;
           
       }
//       if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerStatus>().IsInvincible = true)
//       {
//           m_inputHandler.enabled = false;
//           m_isDisabled = true;
//           m_runTimer = true;
//        }

    }


    void Update() {
            if (m_runTimer) {
                m_timerTimePassed += Time.deltaTime;

                if (m_timerTimePassed >= m_timerLength) {
                    m_timerTimePassed = 0f;
                    if (m_isIce) {
                        m_runTimer = false;
                        m_inputHandler.enabled = true;
                        m_isIce = false;
                    }
                    if (m_isInvincible == true) {
                        m_runTimer = false;
                        m_isInvincible = false;
                    }
                    if (m_isDisabled == true) {
                        m_runTimer = false;
                        m_isDisabled = false;
                    }

                }

            }
        }

    }
