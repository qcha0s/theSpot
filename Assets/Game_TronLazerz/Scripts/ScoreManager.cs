using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public int score_Blue;
	public int score_Red;

	public int ScoreToWin;

	public bool scored_Red;
	public bool scored_Blue;

	public Rigidbody red_puck;
	public Rigidbody blue_puck;
	public Rigidbody green_puck;

	public float hockey_Time;




	// Use this for initialization
	void Start () {
		score_Blue = 0;
		score_Red = 0;
		scored_Blue = false;
		scored_Red = false;
		hockey_Time = 10;
		
	}

	void RespawnPuck(){
			Rigidbody clone;            
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
	void Overtime(){
		hockey_Time = 10;
	}

	void HockeyTimer(){
		hockey_Time = hockey_Time -= Time.deltaTime;
			if(hockey_Time <= 0 && score_Red > score_Blue){
				RedWins();
			}
			if(hockey_Time <= 0 && score_Blue > score_Red){
				BlueWins();
			}
			if(hockey_Time == 0 && score_Blue == score_Red){
				Overtime();
			}
	}

	void RedScore() {
		Debug.Log(score_Red);
		score_Red = score_Red + 1;
		scored_Red = false;
			if(score_Red >= ScoreToWin){
				RedWins();
			}
	}

	void BluedScore() {
		Debug.Log(score_Blue);
		score_Blue = score_Blue + 1;
		scored_Blue = false;
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
			BluedScore();
			RespawnPuck();
		}
		if(scored_Red == true){
			RedScore();
			RespawnPuck();
		}
		HockeyTimer();
		Debug.Log(hockey_Time);
	}
}
