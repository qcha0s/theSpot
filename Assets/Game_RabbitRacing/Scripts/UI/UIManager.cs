using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text m_timerText;
	public Text m_CountDownText;
	private float m_startTime;
	private bool m_isStartGame;

	private void Start() {
		m_timerText.enabled = false;
		//Start Countdown
		StartCoroutine("CountDownTxt");
		m_isStartGame = false;
	}

	private void Update() {
		
		
		//The Countdown Coroutine will set this bool to true and do the following
		if (m_isStartGame) {
			m_timerText.enabled = true;
			
			float t = Time.time - m_startTime;

			string minutes = ((int)t / 60).ToString();
			string seconds = (t % 60).ToString("F2");
		
				//Display the timer for the user. 
				m_timerText.text = minutes + ":" + seconds;
		}
	}
	public void Finnish()
	{
		GameManager.Instance.Isfinnish = true;
		m_timerText.color = Color.yellow;

	}
	//Countdown Coroutine
	IEnumerator CountDownTxt()
	{
		m_CountDownText.enabled = true;
		m_CountDownText.text = "3";
		//playsound
		yield return new WaitForSeconds(2f);
		m_CountDownText.text = "2";
		//playsound
		yield return new WaitForSeconds(2f);
		m_CountDownText.text = "1";
		//playsound
		yield return new WaitForSeconds(2f);
		//playsound
		m_CountDownText.enabled = false;
		m_isStartGame = true;
		m_startTime = Time.time;


	}
}
