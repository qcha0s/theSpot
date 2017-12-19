using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Ultimate : MonoBehaviour {
public const float MAX_PROGRESS = 100.0f;

	public float m_currentProgress;
	public string m_ult;

	private float chargeRate = 1f;
	private BS_Warrior m_warriorScript;
	private BS_Mage m_mageScript;
	private RogueController m_rogueScript;
	
	void Awake() {
		m_warriorScript = GetComponent<BS_Warrior>();
		m_rogueScript = GetComponent<RogueController>();
		m_mageScript = GetComponent<BS_Mage>();
	}

	void start() {
		m_currentProgress = 0;
		Debug.Log(m_warriorScript == null);
	}

	public void Update() {
		
		//m_currentProgress += Time.deltaTime;
		
		// chargeRate;
		if(m_currentProgress >= MAX_PROGRESS && Input.GetKeyDown("3")) {
		Activate();
		}
	}

	public void Activate() {
		Debug.Log("rip");
		
			m_currentProgress = 0;

			switch(gameObject.name){
				case ("Archer"):
				Debug.Log("archer");
				break;
				case ("Mage"):
				//m_mageScript.Ultimate();
				break;
				case ("Warrior"):
				m_warriorScript.Ultimate();
				break;
				case ("Rougue"):
				//m_rogueScript.ShadowStep();
				break;
				default:
				Debug.Log("no class");
				break;
			}
			
			// TODO: add what happens when they die
			Debug.Log("finish him!");
		
	}
	public void Charge(int charger){
		m_currentProgress += charger;
	}
	

	public void MaxOut(int amount) {
		m_currentProgress += amount;
		if(m_currentProgress > MAX_PROGRESS) {
			m_currentProgress = MAX_PROGRESS;
		}
	}
}