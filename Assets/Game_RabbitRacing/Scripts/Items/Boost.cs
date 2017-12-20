using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour, Item{

    //when powerup is activivated increase speed for a period of time.
    private float timerLength = 5f; 
    private float timerTimePassed = 0f;
    private CharacterControllerKart m_baseKartMovement;
    private float m_speed;
    private float m_acc;
    bool runTimer = false;


    public void Use(GameObject user)
    {
        runTimer = true;
        m_baseKartMovement = GetComponent<CharacterControllerKart>();
        m_speed = m_baseKartMovement.Speed;
        m_acc = m_baseKartMovement.m_acceleration;
        m_speed *= 3f;
        m_baseKartMovement.m_acceleration *= 3f;
    }

    void Update()
    {
        if (runTimer)
        {
            timerTimePassed += Time.deltaTime;
           
            if (timerTimePassed >= timerLength)
            {
                timerTimePassed = 0f;
                runTimer = false;
                
                m_baseKartMovement.m_acceleration = m_acc;
                Destroy(gameObject);
            }
        }
    }
}
