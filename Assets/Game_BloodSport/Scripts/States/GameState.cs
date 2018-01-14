using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BS_GameState {

	protected BS_GameManager m_gm;
	public  BS_GameState(BS_GameManager gm) {
		m_gm = gm;
	}

	public abstract void Enter(); 
	public abstract void Execute();
	public abstract void Exit();

}
