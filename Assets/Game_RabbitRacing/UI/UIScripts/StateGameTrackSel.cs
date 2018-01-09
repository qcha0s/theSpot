using UnityEngine;
using UnityEngine.SceneManagement;
namespace RabbitRacing{
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

		//Choose map
		public void loadScene() {
			SceneManager.LoadScene("Scene_01");
		}

	}
}