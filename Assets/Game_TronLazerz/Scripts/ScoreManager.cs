using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int score_Blue;
	public int score_Red;

	public int ScoreToWin;

	public bool scored_Red;
	public bool scored_Blue;

	public bool IsOvertime;

	public bool IsRegular;

	public Rigidbody red_puck;
	public Rigidbody blue_puck;
	public Rigidbody green_puck;

	public float hockey_Time;
	public float m_overtime;
	public float m_wholeAmount;

	public Text countRed;
	public Text backRed;
	public Text countBlue;
	
	public Text countTime;
	




	// Use this for initialization
	void Start () {
		score_Blue = 0;
		score_Red = 0;
		scored_Blue = false;
		scored_Red = false;
		IsOvertime = false;
		IsRegular = true;
		hockey_Time = 10;
		countRed.text = "Score: " + score_Red.ToString();
		countBlue.text = "Score: " + score_Blue.ToString();
		countTime.text = "Time: " + hockey_Time.ToString();
	
		
	}

	void RespawnPuck(){
			Rigidbody clone;

			if(IsRegular == true){           
				if(score_Blue > score_Red){
					clone = Instantiate(blue_puck, transform.position, transform.rotation) as Rigidbody;
				}
				if(score_Blue < score_Red){
					clone = Instantiate(red_puck, transform.position, transform.rotation) as Rigidbody;
				}
				if(score_Blue == score_Red){
					clone = Instantiate(green_puck, transform.position, transform.rotation) as Rigidbody;
				}
			}
		
	}
	void Overtime(){
		hockey_Time = hockey_Time + m_overtime;
		if(IsOvertime == true){
		Rigidbody clone; 
		clone = Instantiate(blue_puck, transform.position, transform.rotation) as Rigidbody;
		clone = Instantiate(red_puck, transform.position, transform.rotation) as Rigidbody;
		
		
		//IsOvertime = false;
		}
		
	}

	void HockeyTimer(){
		hockey_Time = hockey_Time -= Time.deltaTime;
		m_wholeAmount = Mathf.Round(hockey_Time);		
		countTime.text = "Time: " + m_wholeAmount.ToString();
			if(IsRegular == false){
				countTime.text = "OVERTIME"; 
			}
		HockeyEndTimer();
	}

	void HockeyEndTimer(){
			if(hockey_Time <= 0 && score_Red > score_Blue){
				RedWins();
			}
			if(hockey_Time <= 0 && score_Blue > score_Red){
				BlueWins();
			}
			if(hockey_Time <= 0 && score_Blue == score_Red){
				IsOvertime = true;
				IsRegular = false;
				Overtime();
			}
	}

	void RedScore() {
		Debug.Log(score_Red);
		score_Red = score_Red + 1;
		countRed.text = "Score: " + score_Red.ToString();		
		scored_Red = false;
			if(score_Red >= ScoreToWin){
				RedWins();
			}
	}

	void BlueScore() {
		
		score_Blue = score_Blue + 1;
		countBlue.text = "Score: " + score_Blue.ToString();
		scored_Blue = false;
		Debug.Log(score_Blue);
			if(score_Blue >= ScoreToWin){
				BlueWins();
			}
	}

	void BlueWins(){
		Debug.Log("blue");
	}

	void RedWins(){
		Debug.Log("red");
	}

	
	// Update is called once per frame
	void Update () {
		if(scored_Blue == true){
			BlueScore();
			RespawnPuck();
		}
		if(scored_Red == true){
			RedScore();
			RespawnPuck();
		}
		HockeyTimer();
		
	}
}
