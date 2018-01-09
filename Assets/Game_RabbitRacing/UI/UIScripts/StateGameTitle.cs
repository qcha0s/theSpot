using UnityEngine;
namespace RabbitRacing{
	public class StateGameTitle : GameState {

		
		public StateGameTitle(GameManager gm):base(gm) { }
		public override void Enter() { }
		public override void Execute() { }
		public override void Exit() { }

		public void PlayGame() {
			m_gm.NewGameState(m_gm.m_stateGameTrackSel);
		}

		public void QuitGame() {
			Application.Quit();
		}

	}
}