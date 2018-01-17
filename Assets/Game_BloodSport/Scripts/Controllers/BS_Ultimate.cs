using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS_Ultimate : MonoBehaviour {
public const float MAX_PROGRESS = 100.0f;

	public float m_currentProgress;
	public string m_ult;
	public Image m_ultMask;
	public Text m_ultText;
	private float chargeRate = 1f;
	private BS_Warrior m_warriorScript;
	private NetworkedMage m_mageScript;
	private BS_Rogue m_rogueScript;
	private BS_Hunter m_hunterScript;
	
	void Awake() {
		m_warriorScript = GetComponent<BS_Warrior>();
		m_rogueScript = GetComponent<BS_Rogue>();
		m_mageScript = GetComponent<NetworkedMage>();
		m_hunterScript = GetComponent<BS_Hunter>();
	}

	void Start() {
		m_currentProgress = 0;
		
	}

	public void Update() {
		if(m_currentProgress >= MAX_PROGRESS){
			m_ultMask.fillAmount = 0;
			m_ultText.enabled = false;
		}
		else{
			m_ultMask.fillAmount = 1;
			m_ultText.enabled = true;
		}
		if(m_currentProgress >= MAX_PROGRESS && Input.GetKeyDown("3")) {
				Activate();
		}
	}

	public void Activate() {
		m_currentProgress = 0;
		switch(gameObject.name){
			case ("Hunter"):
			m_hunterScript.Ultimate();
			break;
			case ("Mage"):
			//m_mageScript.Ultimate();
			break;
			case ("Warrior"):
			m_warriorScript.Ultimate();
			break;
			case ("Rogue"):
			Debug.Log("Rogue Ults");
			m_rogueScript.UltTarget();
			break;
			default:
			Debug.Log("no class");
			break;
		}
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