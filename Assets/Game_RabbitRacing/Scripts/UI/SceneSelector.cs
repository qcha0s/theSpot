using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour {
private int levelSelector;
public GameObject raceButton;

    void Start(){
        levelSelector = -1;
        raceButton.SetActive(false);
    }

    public void trackOne(){
        levelSelector = 1;
        raceButton.SetActive(true);
    }

    public void trackTwo(){
        levelSelector = 2;
        raceButton.SetActive(true);
    }

    public void trackThree(){
        levelSelector = 3;
        raceButton.SetActive(true);
    }

    public void loadLevel(){
        if(levelSelector != -1){
        
            SceneManager.LoadScene(levelSelector);
        }
    }
}
