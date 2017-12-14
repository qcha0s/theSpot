using UnityEngine;

public class StateGameTrackSel : GameState {

	
	public StateGameTrackSel(GameManager gm):base(gm) { }
	public override void Enter() { }
	public override void Execute() { }
	public override void Exit() { }

	public void PlayGame() {
		m_gm.NewGameState(m_gm.m_stateGamePlay);
	}

	public void QuitGame() {
		Application.Quit();
	}

}
