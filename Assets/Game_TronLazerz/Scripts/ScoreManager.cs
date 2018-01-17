using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public int score_Blue;
	public int score_Red;

	public int ScoreToWin;

	public bool scored_Red;
	public bool scored_Blue;




	// Use this for initialization
	void Start () {
		score_Blue = 0;
		score_Red = 0;
		scored_Blue = false;
		scored_Red = false;
		
	}

	void RedScore() {
		Debug.Log(score_Red);
		score_Red = score_Red + 1;
		scored_Red = false;
			if(score_Red <= ScoreToWin){
				RedWins();
			}
	}

	void BluedScore() {
		Debug.Log(score_Blue);
		score_Blue = score_Blue + 1;
		scored_Blue = false;
			if(score_Blue <= ScoreToWin){
				BlueWins();
			}
	}

	void BlueWins(){

	}

	void RedWins(){

	}

	
	// Update is called once per frame
	void Update () {
		if(scored_Blue == true){
			BluedScore();
		}
		if(scored_Red == true){
			RedScore();
		}
		
	}
}
