//============================================================================================
// Rabbit Racing
// Game Manager Script
//============================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public List<GameObject> m_activePlayers = new List<GameObject>();
    public StateManager m_stateManager;
    public StateGamePlay m_stateGamePlay { get; set; }

    public StateGameWon m_stateGameWon { get; set; }

    //public StateGameLost m_stateGameLost { get; set; }
    public StateGameIntro m_stateGameIntro { get; set; }

    public StateGameTrackSel m_stateGameTrackSel { get; set; }
    public StateGameTitle m_stateGameTitle { get; set; }
    public StateGameCharSel m_stateGameCharSel { get; set; }
    public bool m_isStartGame; 
    private bool m_isfinnish = false;
    private Animator m_animator;
    private GameState m_currentState;

    public static GameManager Instance {
        get { return m_instance; }
    }

    private static GameManager m_instance = null;
    
    
    public bool Isfinnish {
        get { return m_isfinnish; }
        set { m_isfinnish = value; }
    }

    void Awake() {
        
        
        //============================================================================================
        // Rabbit Racing
        // Initialize States
        //============================================================================================
        m_stateGamePlay = new StateGamePlay(this);
        m_stateGameWon = new StateGameWon(this);
        //m_stateGameLost = new StateGameLost(this);
        m_stateGameIntro = new StateGameIntro(this);
        m_stateGameTitle = new StateGameTitle(this);
        m_stateGameCharSel = new StateGameCharSel(this);
        m_stateGameTrackSel = new StateGameTrackSel(this);



    }

    void Start() {
        //singleton
        if (m_instance != null && m_instance != this) {
            Destroy(gameObject);
            return;
        }
        else {
            m_instance = this;

        }
        DontDestroyOnLoad(gameObject);
        //============================================================================================
        // Rabbit Racing
        // Begin Race
        //============================================================================================
        NewGameState(m_stateGameIntro);
        m_activePlayers = new List<GameObject>();
    }

    public void StartGame() {
        NewGameState(m_stateGameIntro);
        Debug.Log("Intro Loaded");
    }

    void Update() {
        if (m_currentState != null) {
            m_currentState.Execute();
        }

        if (m_isfinnish) {

            return;
        }
    }

    public void NewGameState(GameState newState) {
        if (m_currentState != null) {
            m_currentState.Exit();
        }
        m_currentState = newState;
        m_currentState.Enter();
    }

    public void UpdateFSM(GameStates newState) {
        m_stateManager.ChangeState(newState);
    }

}