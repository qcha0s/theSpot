using UnityEngine;

public class StateGameIntro : BS_GameState {

	private float m_countDown = 5f;

	public StateGameIntro(BS_GameManager gm):base(gm) { }

	public override void Enter() { }

	public override void Execute() {
		if(m_countDown <= 0 ){
			m_gm.NewGameState(m_gm.m_stateGameMenu); 
			m_gm.UpdateFSM(GameStates.MENU);

		} else {
			m_countDown -= Time.deltaTime;
		}
	}

	public override void Exit() {}
}
