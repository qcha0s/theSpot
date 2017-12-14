using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Health : MonoBehaviour {

	public const float MAX_HEALTH = 1000.0f;
	public float m_currentHealth;
	public float m_damageReduction = 1f;
	public Slider m_healthBar;
	public Text m_healthText;

	void Start() {
		m_currentHealth = MAX_HEALTH;
		m_healthBar.value = CalculateHealth();
		m_healthText.text = m_currentHealth + "/" + MAX_HEALTH;
	}

	public void TakeDamage(float damage) {
		
		m_currentHealth -= damage * m_damageReduction;
		Die();
		//m_healthBar.value = CalculateHealth();
		//m_healthText.text = m_currentHealth + "/" + MAX_HEALTH;
	}

	public void TakeDotDamage(float dotDmg,float ticks,float tickTime){
		StartCoroutine(Dot(dotDmg,ticks,tickTime));
	}

	public void Die() {
		if(m_currentHealth <= 0) {
			m_currentHealth = 0;
			// TODO: add what happens when they die
			Debug.Log("Dead");
		}
	}

	public void Heal(int amount) {
		m_currentHealth += amount;
		if(m_currentHealth > MAX_HEALTH) {
			m_currentHealth = MAX_HEALTH;
		}
		// m_healthBar.value = CalculateHealth();
		// m_healthText.text = m_currentHealth + "/" + MAX_HEALTH;
	}

	private float CalculateHealth() {
		return m_currentHealth / MAX_HEALTH;
	}

	IEnumerator Dot(float dot,float tickNumber,float timePerTick){
		float appliedTimes = 0;

		yield return new WaitForSeconds(timePerTick);

		 while(appliedTimes < tickNumber)
   	 {
        TakeDamage(dot);
        yield return new WaitForSeconds(timePerTick);
        appliedTimes++;
   	 }
	}
}
