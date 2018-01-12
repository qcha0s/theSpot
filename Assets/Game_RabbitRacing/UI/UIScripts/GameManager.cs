using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public StateManager m_stateManager;
	private GameState m_currentState;
	
	public StateGamePlay m_stateGamePlay { get; set; }
	public StateGameWon m_stateGameWon { get; set; }
	
	//public StateGameLost m_stateGameLost { get; set; }
	public StateGameIntro m_stateGameIntro { get; set; }
	public StateGameTrackSel m_stateGameTrackSel { get; set; }

	public StateGameTitle m_stateGameTitle { get; set; }
	

	public StateGameCharSel m_stateGameCharSel { get; set; }

	public static GameManager Instance { get { return m_instance; } }
	private static GameManager m_instance = null;
	public List<GameObject> m_activePlayers;// = new List<GameObject>();

	void Awake(){
		m_stateGamePlay = new StateGamePlay(this);
		m_stateGameWon = new StateGameWon(this);
		//m_stateGameLost = new StateGameLost(this);
		m_stateGameIntro = new StateGameIntro(this);
		m_stateGameTitle = new StateGameTitle(this);
		m_stateGameCharSel = new StateGameCharSel(this);
		m_stateGameTrackSel = new StateGameTrackSel(this);
		

		
	}
	void Start () {
		NewGameState(m_stateGameIntro);
		m_activePlayers = new List<GameObject>();
		
		//singleton
		if  (m_instance != null && m_instance != this)  {
			Destroy(gameObject);
			return;    
		} else {
			m_instance = this;   
			
		}
		DontDestroyOnLoad(gameObject);  

		
	}

	public void StartGame() {
		NewGameState(m_stateGameIntro);
		Debug.Log("jambi oh yeah");
	}

	void Update(){
		if(m_currentState != null) {
			m_currentState.Execute();
		}
		
			
	}

	public void NewGameState(GameState newState) {
		if(m_currentState != null) {
			m_currentState.Exit();
		}
		m_currentState = newState;
		m_currentState.Enter();
	}

	public void UpdateFSM(GameStates newState) {
		m_stateManager.ChangeState(newState);
	}
	


}
