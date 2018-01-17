using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour {
private int levelSelector;
public GameObject raceButton;
public GameObject forestPreview;
public GameObject snowPreview;
public GameObject desertPreview;

    void Start(){
        levelSelector = -1;
        raceButton.SetActive(false);
        forestPreview.SetActive(false);
        snowPreview.SetActive(false);
        desertPreview.SetActive(false);
        
    }

    public void trackOne(){
        levelSelector = 1;
        forestPreview.SetActive(true);
        snowPreview.SetActive(false);
        desertPreview.SetActive(false);
        raceButton.SetActive(true);
    }

    public void trackTwo(){
        levelSelector = 2;
        snowPreview.SetActive(true);
        forestPreview.SetActive(false);
        desertPreview.SetActive(false);
        raceButton.SetActive(true);
    }

    public void trackThree(){
        levelSelector = 3;
        desertPreview.SetActive(true);
        forestPreview.SetActive(false);
        snowPreview.SetActive(false);
        raceButton.SetActive(true);
    }

    public void loadLevel(){
        if(levelSelector != -1){
        
            SceneManager.LoadScene(levelSelector);
        }
    }
}
