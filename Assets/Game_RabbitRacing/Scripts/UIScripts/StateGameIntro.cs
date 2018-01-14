using UnityEngine;

public class StateGameIntro : GameState {

	private float m_countDown = 4f;

	public StateGameIntro(GameManager gm):base(gm) { }

	public override void Enter() { }

	public override void Execute() {
		Debug.Log("tick");
		if(m_countDown <= 0 ){
			//TODO: Update HUD 
			Debug.Log("Changing State");
			m_gm.NewGameState(m_gm.m_stateGameTitle); 
			m_gm.UpdateFSM(GameStates.TITLE);

		} else {
			m_countDown -= Time.deltaTime;
		}
	}

	public override void Exit() {}
}

