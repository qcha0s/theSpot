using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour, Item {

    //when powerup is activivated increase speed for a period of time.
    private float timerLength = 5f; 
    private float timerTimePassed = 0f;
    private float m_acc;
    private float m_maxSpeed;
    bool runTimer = false;
    CharacterControllerKart m_kartMovement;

    public void Use(GameObject user)
    {
        runTimer = true;
        m_kartMovement = user.GetComponent<CharacterControllerKart>();
        m_kartMovement.m_maxSpeed *= 1.5f;
        m_kartMovement.m_acceleration *= 1.5f;
    }

    void Update()
    {
        if (runTimer)
        {
            timerTimePassed += Time.deltaTime;
           
            if (timerTimePassed >= timerLength)
            {
                m_kartMovement.m_acceleration /= 1.5f;
                m_kartMovement.m_maxSpeed /= 1.5f;
                Destroy(gameObject);
            }
        }
    }
}
