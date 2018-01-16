using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGamePlay : BS_GameState {

	private const int MAX_HITS = 20;
	private bool m_isPaused = false;
	private float m_gameTime = 10f;

	public StateGamePlay(BS_GameManager gm):base(gm) { }

	public override void Enter() {
		m_gameTime = 10f;
		Debug.Log("loose");
		m_gm.ResetStats();
	}

	public override void Execute() {
		{
			m_gm.NewGameState(m_gm.m_stateGameWon);
		}

		m_gameTime -= Time.deltaTime;
		if(m_gameTime <= 0) {
			m_gm.NewGameState(m_gm.m_stateGameLost);
			m_gm.UpdateFSM(GameStates.LOST);


		}

		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(m_isPaused) {
				ResumeGameMode();
			} else {
				PauseGameMode();
			}
		}
	}

	public override void Exit() {
	}

	private void ResumeGameMode() {
		Time.timeScale = 1.0f;
		m_isPaused = false;
	}

	private void PauseGameMode() {
		Time.timeScale = 0.0f;
		m_isPaused = true;
	}
}
