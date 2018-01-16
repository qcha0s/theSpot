using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UD : BaseHealth
{
    private SoundManager_UD m_sound;
    public bool m_isPlayer = false;
    public Image healthBar;

    public float m_respawnTime = 3.0f;

    float m_currentRespawnTime = 4.0f;
    private bool onRegenHealth;

    void Update()
    {

        if (m_isPlayer){
			Debug.Log("health = " + m_currentHealth);
			if((m_currentHealth < 100 && onRegenHealth == false)){
				onRegenHealth = true;
				StartCoroutine(UpdateHealth());
			} else if(m_currentHealth >= 100){
				StopCoroutine(UpdateHealth());
			    m_currentHealth = m_maxHealth;
	    	}
        }

        if (!m_isPlayer)
        {
            healthBar.fillAmount = Health / m_maxHealth;
            healthBar.color = Color.Lerp(m_deadColour, m_healthyColour, healthBar.fillAmount);
        } else
        {
            if (m_isDead)
            {
                if (m_currentRespawnTime >= m_respawnTime)
                {
                    m_isDead = false;
                    GetComponent<Animator>().SetBool("isDead", false);
                    GetComponent<BaseMovement_UD>().enabled = true;
                    GetComponent<CharacterMovement_UD>().enabled = true;
                    GetComponent<RayCastInteraction_UD>().enabled = true;
                    m_currentHealth = m_maxHealth;
					GameManager_UD.instance.UpdatePlayerHPBar(m_currentHealth / m_maxHealth);
                }
                else
                {
                    m_currentRespawnTime += Time.deltaTime;
                }
            }
        }
    }

    	IEnumerator UpdateHealth() {
		m_currentHealth += 1;
		yield return new WaitForSeconds(1);
		onRegenHealth = false;
        GameManager_UD.instance.UpdatePlayerHPBar(m_currentHealth / m_maxHealth);
	}

    public override void Die()
    {
        if (!m_isPlayer)
        {
            m_currentHealth = m_maxHealth;
            m_isDead = false;
            healthBar.color = m_healthyColour;
            gameObject.SetActive(false);
        }
    }

    public override void TakeDamage(float damage)
    {
        if (!m_isDead)
        {
            m_currentHealth -= damage;
            if (m_currentHealth <= 0)
            {
                m_currentHealth = 0;
                if (m_isPlayer)
                {
                    m_isDead = true;
                    
                    GetComponent<Animator>().SetBool("isDead", true);
                    GetComponent<BaseMovement_UD>().enabled = false;
                    GetComponent<CharacterMovement_UD>().enabled = false;
                    GetComponent<RayCastInteraction_UD>().enabled = false;
                    m_currentRespawnTime = 0.0f;
                }
            }
			if (m_isPlayer){
				GameManager_UD.instance.UpdatePlayerHPBar(m_currentHealth / m_maxHealth);
			}

        }
    }
}

