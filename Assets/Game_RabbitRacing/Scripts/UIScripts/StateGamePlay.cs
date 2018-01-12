using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGamePlay : GameState {

	
	private bool m_isPaused = false;
	
	//must figure out how to change this depending on the track
	private float m_gameTime = 45f;

	public StateGamePlay(GameManager gm):base(gm) { }

	public override void Enter() {
		m_gameTime = 45f;
	
	}

	public override void Execute() {
			
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(m_isPaused) {
				ResumeGameMode();
			} else {
				PauseGameMode();
			}
		}
		// TODO: Update HUD
	}

	public override void Exit() {
		// Nothing to see here ... these aren't the droids you're looking for
		// ... move along 
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
