using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour {
private int characterSelector;
public GameObject trackSelButton;
public GameObject character1;
public GameObject character2;
public GameObject character3;
public GameObject character4;

    void Start(){
        characterSelector = -1;
        trackSelButton.SetActive(false);
        
    }

    public void characterOne(){
        characterSelector = 1;
		trackSelButton.SetActive(true);
    }

    public void characterTwo(){
        characterSelector = 2;
       	trackSelButton.SetActive(true);
    }

    public void characterThree(){
        characterSelector = 3;
		trackSelButton.SetActive(true);
    }


    public void characterFour(){
        characterSelector = 4;
		trackSelButton.SetActive(true);
    }
}