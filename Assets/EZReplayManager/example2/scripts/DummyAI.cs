using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummyAI : MonoBehaviour {

	public float speed = 1f;
	public List<GameObject> waypoints = new List<GameObject>();
	public GameObject physicsWrapper;
	private bool alive;
	private GameObject currentWaypoint;
	private float Xtolerance = 1f;
	private float Ytolerance = 1f;
	private float Ztolerance = 1f;
	private float rotationTolerance = 40.0f;
	private bool canMove;
	
	private void Awake() {
		//EZReplayManager.mark4Recording(gameObject);
	}	
	
	// Use this for initialization
	void Start () {
		
		GetComponent<Animation>().wrapMode = WrapMode.Loop;
		currentWaypoint = waypoints[0];
		alive = true;
		canMove = true;
	}
	
	private 	void moveTowards(Vector3 target) {
		if (canMove) {
			//animation.CrossFade("walk");
			Vector3 direction = target - transform.position;
			direction = new Vector3(direction.x,0,direction.z);
		
			physicsWrapper.transform.rotation = Quaternion.LookRotation(direction);
		
		
			// Modify speed so we slow down when we are not facing the target
			var forward = physicsWrapper.transform.TransformDirection(Vector3.forward);

			// Move the character
			physicsWrapper.transform.position += forward * speed * 0.05f;
		}	
		/*else {
			animation.CrossFade("idle");
		}*/
	}
	
	private IEnumerator recoverFromHit(float duration) {
		yield return new WaitForSeconds(duration);
		canMove = true;
	}
	
	public void hit(float strength) {

		if (strength <1000) {
			canMove = false;
			StartCoroutine(recoverFromHit(0.4f));
		} else {
			canMove = false;
			alive = false;
		}
	}
	
	private IEnumerator replay() {
		yield return new WaitForSeconds(4f);
		EZReplayManager.get.play(0,true,true,false);
	}	
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Transform t = physicsWrapper.transform;
		if (canMove) {
			if(alive && ((t.localEulerAngles.x > rotationTolerance && t.localEulerAngles.x < 360.0f - rotationTolerance)
				|| (t.localEulerAngles.x < rotationTolerance * -1 && t.localEulerAngles.x > -360.0f + rotationTolerance) ||   
				(t.localEulerAngles.z > rotationTolerance && t.localEulerAngles.z < 360.0f - rotationTolerance)
				|| (t.localEulerAngles.z < rotationTolerance * -1 && t.localEulerAngles.z > -360.0f + rotationTolerance) ))	{
					alive=false;
					GameObject game = GameObject.Find("Game");
					game.GetComponent<Monitor>().setActive(false);
					StartCoroutine(replay());
			}				
		}
		
		if (alive && canMove) 
			GetComponent<Animation>().CrossFade("walk");
		else
			GetComponent<Animation>().CrossFade("idle");
		
		//gameObject.GetComponent <CharacterController>().SimpleMove(new Vector3(0,0,0));
		if (alive) {
			
			Vector3 curPos = transform.position;
			Vector3 curWaypP = currentWaypoint.transform.position;
			if (curPos.x > curWaypP.x - Xtolerance && curPos.x < curWaypP.x + Xtolerance 
					&& curPos.y > curWaypP.y - Ytolerance && curPos.y < curWaypP.y + Ytolerance 
					&& curPos.z > curWaypP.z - Ztolerance && curPos.z < curWaypP.z + Ztolerance 
				) {
					
					int nextWPno = -1;
					for(int i=0;i<waypoints.Count;i++) {
						GameObject wp = waypoints[i];
						if (wp == currentWaypoint) {
							nextWPno = i+1;
							break;
						}
					}
					if (nextWPno > waypoints.Count-1)
						nextWPno = 0;
					currentWaypoint = waypoints[nextWPno];
			} 
				
			moveTowards(currentWaypoint.transform.position);

					
			
		} else {
			//transform.parent = null;
			GetComponent<Animation>().CrossFade("idle");
		}
	}
}
