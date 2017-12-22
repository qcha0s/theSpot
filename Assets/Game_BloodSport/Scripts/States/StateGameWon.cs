using UnityEngine;

public class StateGameWon : BS_GameState {

	private float m_countDown = 5f;

	public StateGameWon(BS_GameManager gm):base(gm) { }

	public override void Enter() {
		m_countDown = 5f; 
	}

	public override void Execute() {
		if(m_countDown <= 0 ){
			m_gm.NewGameState(m_gm.m_stateGameMenu); 

		} else {
			m_countDown -= Time.deltaTime;
		}
	}

	public override void Exit() {}
}
