using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Intro, Menu, Loading, Play, Win, Lose }
public enum GameMode { Normal, Survival }
public enum Tower { Arrow, Cannon, Tar }
public class GameManager_UD : MonoBehaviour
{



    //public
    public static GameManager_UD instance;

    public float m_introTimer = 3.0f;

    public GameState m_currentState;

    public GameObject[] m_gameStates;

    public Dropdown m_mapDropdown;

    public GameObject[] m_maps; //image of maps

    public string[] m_mapSceneName;

    public GameObject m_lockImage;
    public GameObject m_normalButton;
    public GameObject m_survivalButton;
    public Text m_highscoreText;

    public GameObject m_pauseUI;

    public GameObject m_poolManager;

    public GameObject m_buildTowerUI;

	public Button[] m_buildTowerButtons;

    public int[] m_startGold;

    public Text m_goldText;

    public int[] m_towerCost;

    public Text m_wavesText;

    //enemy drops 1 gold
    //30 gold for a tower

    //private
    float m_currentIntroTimer;

    int m_currentSelectedLevel = 0;

    int[] m_mapUnlockState = new int[3];

    //unlock states
    //0 - locked
    //1 - unlocked - normal
    //2 - unlocked - survival

	int[] m_TowerUnlockState = new int[3];

    int[] m_bestWave = new int [3];

    GameMode m_currentGameMode = GameMode.Normal;

    bool m_showMouse;

    BuildSpot_UD m_currentSpot;

    CharacterMovement_UD playerMovementScript;

    int m_currentGold = 0;

    bool m_paused = false;

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

        m_currentIntroTimer = 0.0f;

        m_mapUnlockState[0] = PlayerPrefs.GetInt("Map1");
        m_mapUnlockState[1] = PlayerPrefs.GetInt("Map2");
        m_mapUnlockState[2] = PlayerPrefs.GetInt("Map3");

		m_TowerUnlockState[0] = PlayerPrefs.GetInt("Tower1");
		m_TowerUnlockState[1] = PlayerPrefs.GetInt("Tower2");
		m_TowerUnlockState[2] = PlayerPrefs.GetInt("Tower3");

        m_bestWave[0] = PlayerPrefs.GetInt("BestWave1");
        m_bestWave[1] = PlayerPrefs.GetInt("BestWave2");
        m_bestWave[2] = PlayerPrefs.GetInt("BestWave3");

        Debug.Log(m_mapUnlockState[0]);
        Debug.Log(m_mapUnlockState[1]);
        Debug.Log(m_mapUnlockState[2]);

        Debug.Log(m_TowerUnlockState[0]);
        Debug.Log(m_TowerUnlockState[1]);
        Debug.Log(m_TowerUnlockState[2]);

        if (m_mapUnlockState[0] == 0){
            ResetStats();            
        }

        ShowMouse();

    }
    void Start()
    {
        m_currentState = GameState.Intro;
        foreach (GameObject go in m_gameStates)
        {
            go.SetActive(false);
        }
        m_gameStates[(int)m_currentState].SetActive(true);
        ChangeSelectedLevel();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_currentState)
        {
            case GameState.Intro:
                UpdateIntro();
                break;
            case GameState.Menu:
                break;
            case GameState.Loading:
                break;
            case GameState.Play:
                UpdatePlay();
                break;
        }
    }

    void UpdateIntro()
    {
        if (m_currentIntroTimer > m_introTimer)
        {
            ChangeState(GameState.Menu);
        }
        else
        {
            m_currentIntroTimer += Time.deltaTime;
        }

    }

    void UpdatePlay()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (!m_paused){
                PauseGame();
            }
            else{
                ResumeGame();
            }
        }
    }

    void ChangeState(GameState newState)
    {
        m_gameStates[(int)m_currentState].SetActive(false);
        m_currentState = newState;
        m_gameStates[(int)m_currentState].SetActive(true);
    }

    //when player changes level in the dropdown
    public void ChangeSelectedLevel()
    {
        m_maps[m_currentSelectedLevel].SetActive(false);


        m_currentSelectedLevel = m_mapDropdown.value;

        switch (m_mapUnlockState[m_currentSelectedLevel])
        {
            case 0://locked
                m_lockImage.SetActive(true);
                m_normalButton.SetActive(false);
                m_survivalButton.SetActive(false);
                //TODO:update score
                break;
            case 1://unlocked - normal
                m_lockImage.SetActive(false);
                m_normalButton.SetActive(true);
                m_survivalButton.SetActive(false);
                //TODO:update score
                break;
            case 2://unlocked - survival
                m_lockImage.SetActive(false);
                m_normalButton.SetActive(true);
                m_survivalButton.SetActive(true);
                //TODO:update score
                break;
        }
        m_maps[m_currentSelectedLevel].SetActive(true);

        m_highscoreText.text = "Best\n"+m_bestWave[m_currentSelectedLevel];
    }

    public void StartNormal()
    {
        //TODO:change level
        m_currentGameMode = GameMode.Normal;
        ChangeState(GameState.Loading);
        LoadLevel(m_mapSceneName[m_currentSelectedLevel]);

    }

    public void StartSurvival()
    {
        //TODO:change level
        m_currentGameMode = GameMode.Survival;
        ChangeState(GameState.Loading);
        LoadLevel(m_mapSceneName[m_currentSelectedLevel]);
    }

    void LoadLevel(string scene)
    {   
        SceneManager.LoadScene(scene);
        //don't instantiate objects here	

    }
    
    //Once the map loads
    public void MapReady()
    {
        Instantiate(m_poolManager);
        m_currentGold = m_startGold[m_currentSelectedLevel];
        UpdateGold();
        ChangeState(GameState.Play);
        HideMouse();
        m_pauseUI.SetActive(false);
        m_paused = false;
        Time.timeScale = 1.0f;

    }

    public void ShowBuildTowerUI(BuildSpot_UD script)
    {
        m_currentSpot = script;
		UpdateUnlockedTowers();
        m_buildTowerUI.SetActive(true);
        ShowMouse();
        playerMovementScript.enabled = false;
        playerMovementScript.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Camera.main.GetComponent<BS_ThirdPersonCamera>().enabled = false;
    }
    public void HideBuildTowerUI()
    {
        m_currentSpot = null;
        m_buildTowerUI.SetActive(false);
        HideMouse();
        playerMovementScript.enabled = true;
        Camera.main.GetComponent<BS_ThirdPersonCamera>().enabled = true;
    }

    public void BuildArrowTower()
    {
		Debug.Log((int)Tower.Arrow);
        //0
        if (m_currentGold >= m_towerCost[(int)Tower.Arrow])
        {
			m_currentGold -= m_towerCost[(int)Tower.Arrow];
            m_currentSpot.BuildTower((int)Tower.Arrow);
			UpdateGold();
        }

        HideBuildTowerUI();
    }

    public void BuildCannonTower()
    {
        //1
        if (m_currentGold >= m_towerCost[(int)Tower.Cannon])
        {
			m_currentGold -= m_towerCost[(int)Tower.Cannon];
            m_currentSpot.BuildTower((int)Tower.Cannon);
			UpdateGold();
        }
        HideBuildTowerUI();
    }

    public void BuildTarTower()
    {
        //2
        if (m_currentGold >= m_towerCost[(int)Tower.Tar])
        {
			m_currentGold -= m_towerCost[(int)Tower.Tar];
            m_currentSpot.BuildTower((int)Tower.Tar);
			UpdateGold();
        }
        HideBuildTowerUI();
    }

    void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        m_showMouse = true;
    }

    void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_showMouse = false;
    }

    public void SetPlayerMovementScript(CharacterMovement_UD script)
    {
        playerMovementScript = script;
    }

    void UpdateGold()
    {
        //m_goldText.text = "Currency: " + m_currentGold;
        m_goldText.text = m_currentGold.ToString("0");
        Debug.Log(m_goldText.text);
    }

	void UpdateUnlockedTowers(){
		for (int i = 0;i<m_buildTowerButtons.Length;i++){
			if (m_currentGold < m_towerCost[i] || m_TowerUnlockState[i] == 0){
				m_buildTowerButtons[i].interactable = false;
			}
			else{
				m_buildTowerButtons[i].interactable = true;
			}
		}
	}

    public void UpdateWavesText(int wave){
        switch(m_currentGameMode){
            case GameMode.Normal:
                m_wavesText.text = "Wave "+wave+" of "+WaveManager.instance.m_maxWavesNormal;
            break;
            case GameMode.Survival:
                m_wavesText.text = "Wave "+wave;
            break;
        }
    }
    
    public GameMode GetMode(){
        return m_currentGameMode;
    }

    public void AddGold(int amount) {
        m_currentGold += amount;
        UpdateGold();
    }


    public void GameWin(){
        ShowMouse();
        ChangeState(GameState.Win);
        if (m_currentGameMode == GameMode.Normal){
            //selected level - map
            //0 - 1
            //1 - 2
            //2 - 3
            Debug.Log("selected level:"+m_currentSelectedLevel);
            switch(m_currentSelectedLevel){
                case 0:
                    m_mapUnlockState[0] = 2;
                    PlayerPrefs.SetInt("Map1",2);

                    m_mapUnlockState[1] = 1;
                    PlayerPrefs.SetInt("Map2",1);
                    break;
                case 1:
                    m_mapUnlockState[1] = 2;
                    PlayerPrefs.SetInt("Map2",2);

                    m_mapUnlockState[2] = 1;
                    PlayerPrefs.SetInt("Map3",1);
                    
                    m_TowerUnlockState[1] = 1;
                    PlayerPrefs.SetInt("Tower2",1);
                break;
                case 2:
                    m_mapUnlockState[2] = 2;
                    PlayerPrefs.SetInt("Map3",2);
                    
                    m_TowerUnlockState[2] = 1;
                    PlayerPrefs.SetInt("Tower3",1);
                break;
            }
        }
        //TODO:do checks for unlocks
    }

    public void GameLose(){
        ShowMouse();
        ChangeState(GameState.Lose);
        //track survived waves in surivival
    }
    public void ButtonMainMenu(){
        ChangeState(GameState.Loading);
        LoadLevel("MainMenu_UD");
        ChangeState(GameState.Intro);
        m_currentIntroTimer = 0.0f;
        ChangeSelectedLevel();
        Time.timeScale = 1.0f;
    }

    public void ButtonReset(){
        ResetStats();
        ChangeSelectedLevel();
    }

    void ResetStats(){
        m_mapUnlockState[0] = 1;
        m_mapUnlockState[1] = 0;
        m_mapUnlockState[2] = 0;

        m_TowerUnlockState[0] = 1;
		m_TowerUnlockState[1] = 0;
		m_TowerUnlockState[2] = 0;

        m_bestWave[0] = 0;
        m_bestWave[1] = 0;
        m_bestWave[2] = 0;

        PlayerPrefs.SetInt("Map1",1);
        PlayerPrefs.SetInt("Map2",0);
        PlayerPrefs.SetInt("Map3",0);

        PlayerPrefs.SetInt("Tower1",1);
        PlayerPrefs.SetInt("Tower2",0);
        PlayerPrefs.SetInt("Tower3",0);

        PlayerPrefs.SetInt("BestWave1",0);
        PlayerPrefs.SetInt("BestWave2",0);
        PlayerPrefs.SetInt("BestWave3",0);

        //TODO:reset waves survived
    }

    void PauseGame(){
        m_paused = true;
        Time.timeScale = 0.0f;
        ShowMouse();
        m_pauseUI.SetActive(true);
    }

    public void ResumeGame(){
        m_paused = false;
        Time.timeScale = 1.0f;
        HideMouse();
        m_pauseUI.SetActive(false);
    }
}
