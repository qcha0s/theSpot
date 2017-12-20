using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour, Item{

    //when powerup is activivated increase speed for a period of time.
    private float timerLength = 5f; 
    private float timerTimePassed = 0f;
    private CharacterControllerKart m_baseKartMovement;
    private float m_acc;
    private float m_maxSpeed;
    bool runTimer = false;


    public void Use(GameObject user)
    {
        runTimer = true;
        m_baseKartMovement = GetComponent<CharacterControllerKart>();
        m_acc = m_baseKartMovement.Acceleration;
        m_maxSpeed = m_baseKartMovement.MaxSpeed;
        m_acc = 25f;
        m_maxSpeed = 50f;
   
    }

    void Update()
    { Debug.Log(m_acc);
        
        if (runTimer)
        {
            timerTimePassed += Time.deltaTime;
           
            if (timerTimePassed >= timerLength)
            {
                timerTimePassed = 0f;
                runTimer = false;
                m_acc = 8f;
                m_maxSpeed = 25f;
                //m_baseKartMovement.m_acceleration = m_acc;
                Destroy(gameObject);
            }
        }
    }
}
