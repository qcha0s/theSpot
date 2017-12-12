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

    public GameObject m_lockImage;
    public GameObject m_normalButton;
    public GameObject m_survivalButton;

    public GameObject m_poolManager;

    public GameObject m_buildTowerUI;

	public Button[] m_buildTowerButtons;

    public int[] m_startGold;

    public Text m_goldText;

    public int[] m_towerCost;

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
    int m_map1 = 1;
    int m_map2 = 0;
    int m_map3 = 0;

	int[] m_TowerUnlockState = new int[3];

    GameMode m_currentGameMode = GameMode.Normal;

    bool m_showMouse;

    BuildSpot_UD m_currentSpot;

    CharacterMovement_UD playerMovementScript;

    int m_currentGold = 0;

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

        m_mapUnlockState[0] = 1;
        m_mapUnlockState[1] = 0;
        m_mapUnlockState[2] = 0;

        m_map1 = PlayerPrefs.GetInt("Map1");
        m_map2 = PlayerPrefs.GetInt("Map2");
        m_map3 = PlayerPrefs.GetInt("Map3");

		m_TowerUnlockState[0] = 1;
		m_TowerUnlockState[1] = 0;
		m_TowerUnlockState[2] = 0;

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
    }

    public void StartNormal()
    {
        //TODO:change level
        m_currentGameMode = GameMode.Normal;
        ChangeState(GameState.Loading);
        LoadLevel(m_currentSelectedLevel);

    }

    public void StartSurvival()
    {
        //TODO:change level
        m_currentGameMode = GameMode.Survival;
        ChangeState(GameState.Loading);
        LoadLevel(m_currentSelectedLevel);
    }

    void LoadLevel(int scene)
    {
        SceneManager.LoadScene(scene + 1);
        //don't instantiate objects here	

    }
    public void MapReady()
    {
        Instantiate(m_poolManager);
        m_currentGold = m_startGold[m_currentSelectedLevel];
        UpdateGold();
        ChangeState(GameState.Play);
        HideMouse();
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
        m_goldText.text = "Currency: " + m_currentGold;
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
}
