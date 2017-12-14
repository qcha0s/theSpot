using UnityEngine;

public enum GameStates { INTRO, TITLE, CHARSEL, TRACKSEL, PLAY, WON }


public class StateManager : MonoBehaviour {

	
	public GameObject[] m_gameStates; 
	private GameStates m_activeState; 
	private int m_numStates;

	void Awake() {
		m_numStates = m_gameStates.Length;
		
		for(int i = 0; i < m_numStates; i++) {
			m_gameStates[i].SetActive(false);
		}

		m_activeState = GameStates.INTRO;
		m_gameStates[(int)m_activeState].SetActive(true); // 0 == menu	
		 
		//GameManager.Instance.StartGame();
		 
		//((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
	}

	void Start(){
		GameManager.Instance.StartGame();
	}

	public void ChangeState(GameStates newState){
		m_gameStates[(int)m_activeState].SetActive(false);
		m_activeState = newState;
		m_gameStates[(int)m_activeState].SetActive(true);
	}


	public void PlayGame() {
		GameManager.Instance.m_stateGameTrackSel.PlayGame();
		m_gameStates[(int)m_activeState].SetActive(false);
		m_activeState = GameStates.PLAY;
		m_gameStates[(int)m_activeState].SetActive(true);
	}

	public void QuitGame() {
		GameManager.Instance.m_stateGameTrackSel.QuitGame();
	}


}

