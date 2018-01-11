using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour {

	[SyncVar (hook="UpdateBlueText")]
	public int score_Blue;
	[SyncVar (hook="UpdateRedText")]
	public int score_Red;

	public int ScoreToWin;

	public bool scored_Red;
	public bool scored_Blue;

	public bool IsOvertime;

	public bool IsRegular;

	public GameObject green_puck;

	public float hockey_Time;
	public float m_overtime;
	public float m_wholeAmount;

	public Text countRed;

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
		hockey_Time = 60;
		countRed.text = "Score: " + score_Red.ToString();
		countBlue.text = "Score: " + score_Blue.ToString();
		countTime.text = "Time: " + hockey_Time.ToString();
		
	
		
	}
	private void UpdateRedText(int score){
		countRed.text = "Score: " + score.ToString();
	}

	private void UpdateBlueText(int score){
		countBlue.text = "Score: " + score.ToString();
	}
	

	void RespawnPuck(){
		
		GameObject clone = null;
		if(isServer){
			if(IsRegular == true){           
				clone = Instantiate(green_puck, transform.position, transform.rotation);
				CmdSpawnObject(clone);					
			}
		}
	}

	
	void Overtime(){
		hockey_Time = hockey_Time + m_overtime;
		if(IsOvertime == true){
			GameObject clone; 
			clone = Instantiate(green_puck, transform.position, transform.rotation);
			CmdSpawnObject(clone);
			clone = Instantiate(green_puck, transform.position, transform.rotation);
			CmdSpawnObject(clone);
		
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

	[Command]
	private void CmdSpawnObject(GameObject obj){
		NetworkServer.Spawn(obj);

	}
	
	private void CmdUpdateScore(bool scored_Red){
		if(scored_Red){
			score_Red = score_Red + 1;
		}else{
			score_Blue = score_Blue + 1;
		}
	}
}
