using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	//the minimum strength the player shoots with
	private const int minShootingPower= 1;
	//the current shooting power
//	private int shootingPower= minShootingPower;
	//the shooting power steps to increase with
//	private int shootingPowerStep= 1;
	//is this player the local (controllable) human?
	public bool isLocalHuman = false;
	//is this player alive?
	public bool isAlive = true;
	//player number
	public int playerNo = -1;
	//current score
	public int score;
	//current projectile
	public GameObject projectile;
	
	private void Awake() {
		//EZReplayManager.mark4Recording(gameObject);
	}
	
	private void  Start (){
		
	}

	public void setPlayerNo(int no) {
		playerNo = no;
	}
	
	public int getPlayerNo() {
		return playerNo;
	}
	
	//catch if the player is shooting (normally left mouse button) and perform other checks
	//has to be lateUpdate because otherwise some events are not catched:
	private void  LateUpdate (){
		GameObject game = GameObject.Find("Game");
		
		//game active and player human?
		if (isLocalHuman && game.GetComponent<Monitor>().isActive() && EZReplayManager.get.getCurrentMode() != ViewMode.REPLAY) {
			
			/*if (Input.GetButton ("Fire1")) {
				//increase firing power
				shootingPower +=shootingPowerStep;				
			}*/
			
			if (Input.GetButtonUp ("Fire1")) {
				//fire!
				//shoot(shootingPower);
				shoot(40);
//				shootingPower = minShootingPower;
			}	 
		}
	

	}

	//the player fires a projectile
	public void  shoot (float shootingPower){

			if (isAlive) {
				GameObject shaft= (GameObject)transform.Find("Canon").transform.Find("Shaftbase").transform.Find("Shaft").gameObject;
				Vector3 shaftPos= shaft.transform.position;
				float shaftYScale= shaft.transform.localScale.y;
				//where should the projectile be instantiated?
				Vector3 targetPos= shaftPos + shaft.transform.TransformDirection( 0,shaftYScale+0.1f,0 );
				GameObject instantiatedProjectile = (GameObject)Instantiate(projectile, targetPos, transform.rotation );
				//shoot ahead:
				instantiatedProjectile.GetComponent<Rigidbody>().velocity =	shaft.transform.TransformDirection( 0,shootingPower,0 );
			}
		
	}


}