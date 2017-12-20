using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour, Item {

    private float timerLength = 5f;
    private float timerTimePassed = 0f;
    private PlayerStatus m_playerStatus;
    bool runTimer = false;

    public void Use(GameObject user)
    {
        runTimer = true;
        m_playerStatus.IsInvincible = true;
    }

    void Update()
    {
        if (runTimer)
        {
            timerTimePassed += Time.deltaTime;

            if (timerTimePassed >= timerLength)
            {
                m_playerStatus.IsInvincible = false;
                timerTimePassed = 0f;
                runTimer = false;
                Destroy(gameObject);
            }
        }
    }
}
