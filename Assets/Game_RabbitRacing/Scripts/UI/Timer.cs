using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text m_timerText;
    public Text m_CountDownText;
    public bool m_isStartGame; //Todo this will be in game manager later
    private bool m_isfinnish = false;
    private float m_startTime;
    private Animator m_animator;

    private float m_countDownTimeRemaining = 100;
	// Use this for initialization
	void Start () {
	   
        //Store the time so we can have a Lap time and Race time.
        m_startTime = Time.time;
	    m_timerText.enabled = false;
        //3, 2 , 1 GO !
        StartCoroutine("CountDownTxt");
    }
	
	// Update is called once per frame
	void Update () {
	    if (m_isfinnish) {
	        return;
	    }
	 
        //The Countdown Coroutine will set this bool to true and do the following
	    if (m_isStartGame) {
	        m_timerText.enabled = true;
	        float t = Time.time - m_startTime;

	        string minutes = ((int) t / 60).ToString();
	        string seconds = (t % 60).ToString("F2");
	        string countdown =
            
            //Display the timer for the user. 
    	    m_timerText.text = minutes + ":" + seconds;
	        

	    }
	}
    //TODO send a message to this from the finnish line.
    //TODO finnish line will have Ontrigger which will store lap time and a Tag to initiate completion of Laps and Race
   

    public void Finnish() {
        m_isfinnish = true;
        m_timerText.color = Color.yellow;

    }
    //Countdown Coroutine
    IEnumerator CountDownTxt() {
        m_CountDownText.enabled = true;
        m_CountDownText.text = "3";
        yield return new WaitForSeconds(2f);
        m_CountDownText.text = "2";
        yield return new WaitForSeconds(2f);
        m_CountDownText.text = "1";
        yield return new WaitForSeconds(2f);
        m_CountDownText.enabled = false;
        m_isStartGame = true;


    }
}
