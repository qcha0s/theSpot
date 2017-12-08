using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { Intro, Menu, Play, Win, Lose, OverTime }

public class GameManager : MonoBehaviour
{

public static GameManager instance;

public GameState m_currentState;

public GameObject[] m_gameStates;
public GameObject[] m_paddle;
public GameObject m_pauseMenu;
public Dropdown m_dropdownPaddle;

public GameObject m_timer;
//public GameObject m_controlsUI;

float m_introTimer = 1.0f;
//public int m_paddleSelected = 0;//must be public for spawning
int m_RinkSelected = 0;
GameObject m_currentPlayer = null;

bool m_paused = false;

//bool m_showControls = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < m_gameStates.Length; i++)
        {
            m_gameStates[i].SetActive(false);
        }
        Debug.Log((int)GameState.Intro);
        m_currentState = GameState.Intro;
        m_gameStates[(int)GameState.Intro].SetActive(true);
        m_introTimer = 2.0f;
       // GamePaused();

    }

    void Update()
    {
        switch (m_currentState)
        {
            case GameState.Intro:

                UpdateIntro();
                break;
            case GameState.Menu:
                UnlockMouse();
                break;
            case GameState.Play:
                UpdatePlay();
                break;
            case GameState.Win:
                UnlockMouse();
                break;
            case GameState.Lose:
                UnlockMouse();
                break;
			case GameState.OverTime:
                UnlockMouse();
                break;
        }


    }

    void UpdateIntro()
    {
        if (m_introTimer <= 0.0f)
        {
            //  Debug.Log("Change to MENU");
            ChangeGameState(GameState.Menu);
        }
        else
        {
            m_introTimer -= Time.deltaTime;
        }
    }

    void UpdatePlay()
    {UnPause();
        m_timer.GetComponent<DigitalCountdown>();
        // if (m_gameTimer > 0.0f)
        // {
        //     m_gameTimer -= Time.deltaTime;
        //     if (m_gameTimer < 0)
        //     {
        //         m_gameTimer = 0;
        //         GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>().PlayerDie();
        //         ChangeGameState(GameState.Lose);
        //     }
        //     m_gameTimerText.text = Mathf.Floor((m_gameTimer / 60)).ToString("0") + ":" + Mathf.Floor((m_gameTimer % 60)).ToString("00");
        // }
        // if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        // {
        //     Debug.Log("escape");
        //     Debug.Log(m_paused);
        //     if (m_paused)
        //     {
        //         UnPause();
        //     }
        //     else
        //     {
        //         Pause();
        //     }
        // }

    }

    public void StartGameBtn(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    void ChangeGameState(GameState newState)
    {
        m_gameStates[(int)m_currentState].SetActive(false);
        m_currentState = newState;
        m_gameStates[(int)m_currentState].SetActive(true);
    }

    public void MenuButtonPlay()
    {
        ChangeGameState(GameState.Play);
        SceneManager.LoadScene(m_RinkSelected);
       // m_gameTimer = m_gameLength;
        LockMouse();
        m_pauseMenu.SetActive(false);
    }

    public void ChangeCharacter()
    {
        //Debug.Log("Menu changed char");
       // m_playerSelected = m_dropdownCharacter.value;
    }

    public void ChangeLevel()
    {
        // Debug.Log("Menu changed level");
       // m_levelSelected = m_dropdownLevel.value + 1;
    }
	public void GameOverTime(){
		if(m_currentState == GameState.OverTime){
			Debug.Log("gamedrawNeedsOT");
			ChangeGameState(GameState.OverTime);
		}
	}

    public void GameLose()
    {
        Debug.Log(m_currentState);
        if (m_currentState == GameState.Play)
        {
            Debug.Log("Game Over");
            ChangeGameState(GameState.Lose);
        }

    }

    public void GameWin()
    {
        Debug.Log(m_currentState);
        if (m_currentState == GameState.Play)
        {
            Debug.Log("Win");
            ChangeGameState(GameState.Win);
        }

    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        ChangeGameState(GameState.Menu);
        

    }

    public void SetPlayer(GameObject player)
    {
        m_currentPlayer = player;
    }

    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_paused = false;
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        m_paused = true;
    }

    public bool GamePaused()
    {
        return m_paused;
    }

    void Pause()
    {
        Debug.Log("timescale 0");
        Time.timeScale = 0.0f;
        UnlockMouse();
//        GameObject.Find("theme").GetComponent<AudioSource>().Pause();
        m_pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        Debug.Log("timescale 1");
        Time.timeScale = 1.0f;
        LockMouse();
       // GameObject.Find("theme").GetComponent<AudioSource>().UnPause();
        m_pauseMenu.SetActive(false);
    }

    public void ToggleControlsUI(){
        // m_showControls = !m_showControls;

        // m_controlsUI.SetActive(m_showControls);
        
    }
}