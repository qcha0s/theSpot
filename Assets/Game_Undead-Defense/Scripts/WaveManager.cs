using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	public static WaveManager instance;

	public float m_countdownLength = 5.0f;

	public int m_maxWavesNormal = 5;

	//Normal:
	//first map - 5 waves
	//second map - 7 waves
	//third map - 10 waves

	EnemySpawner_UD m_enemySpawner;

	int m_currentWave = 0;

	int m_numEnemies = 0;

	float m_countdown = 0;

	void Awake(){
        instance = this;
	}

	void Start () {
		GameManager_UD.instance.m_wavesText.text = "Press Mouse3 to start wave";
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(2) && m_currentWave == 0){
			Debug.Log("Mouse 3 pressed");
			m_currentWave = 1;
			m_numEnemies = m_enemySpawner.SpawnEnemies(m_currentWave);
			UpdateWaveText();

		}

		if(m_currentWave > 0 && m_countdown < m_countdownLength && m_numEnemies == 0){
			GameManager_UD.instance.m_wavesText.text = "Next wave in "+ Mathf.Ceil(m_countdownLength - m_countdown).ToString("0");
			m_countdown += Time.deltaTime;
		}
		else if(m_countdown >= m_countdownLength){
			m_countdown = 0;
			m_numEnemies = m_enemySpawner.SpawnEnemies(++m_currentWave);
			UpdateWaveText();
		}

	}

	public void EnemyDied(){
		m_numEnemies--;

		if (GameManager_UD.instance.GetMode() == GameMode.Normal && m_numEnemies <= 0 && m_currentWave == m_maxWavesNormal){
			//end game
		}
		else if (m_numEnemies <= 0){
			m_countdown = 0;
		}
	}

	void UpdateWaveText(){
		GameManager_UD.instance.UpdateWavesText(m_currentWave);
	}
}
