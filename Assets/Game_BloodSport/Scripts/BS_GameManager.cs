using System.Collections;
using UnityEngine;

public class BS_GameManager : MonoBehaviour {

	
	// A FSM for Game States
	public StateManager m_stateManager;
	private BS_GameState m_currentState; 
	public StateGamePlay m_stateGamePlay { get; set; }
	public StateGameWon m_stateGameWon { get; set; }
	public StateGameLost m_stateGameLost { get; set; }
	public StateGameIntro m_stateGameIntro { get; set; }
	public StateGameMenu m_stateGameMenu { get; set; }


	public static BS_GameManager Instance { get { return m_instance; } }
	private static BS_GameManager m_instance = null;

	void Start() {
		if(m_instance != null && m_instance != this) {
			Destroy(this.gameObject);
			return; 
		} else {
			m_instance = this;
		}
		DontDestroyOnLoad(this.gameObject);

		m_stateGamePlay = new StateGamePlay(this);
		m_stateGameWon = new StateGameWon(this);
		m_stateGameLost = new StateGameLost(this);
		m_stateGameIntro = new StateGameIntro(this);
		m_stateGameMenu = new StateGameMenu(this);
	}

	public void StartGame() {
		NewGameState(m_stateGameIntro);
	}

	private void Update() {
		if(m_currentState != null) {
			m_currentState.Execute();
		}
	}

	public void NewGameState(BS_GameState newState) {
		if(m_currentState != null) {
			m_currentState.Exit();
		}
		m_currentState = newState;
		m_currentState.Enter();
	}

	public void UpdateFSM(GameStates newState) {
		m_stateManager.ChangeState(newState);
	}

	public void ResetStats(){

	}

}

