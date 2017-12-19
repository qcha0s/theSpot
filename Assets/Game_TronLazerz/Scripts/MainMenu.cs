
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {



public Animator CameraObject;



public GameObject hoverSound;
public GameObject sfxhoversound;
public GameObject clickSound;
public GameObject areYouSure;



// highlights


public GameObject lineGeneral;


public void StartGameBtn(string sceneName){
        
        SceneManager.LoadScene(sceneName);
    }




public void  Position2 (){
		Debug.Log(CameraObject);
	
	CameraObject.SetFloat("Animate",1);
}

public void  Position1 (){
	CameraObject.SetFloat("Animate",0);
}
public void  PlayHover (){
	hoverSound.GetComponent<AudioSource>().Play();
}

public void  PlaySFXHover (){
	sfxhoversound.GetComponent<AudioSource>().Play();
}

public void  PlayClick (){
	clickSound.GetComponent<AudioSource>().Play();
}

public void  AreYouSure (){
	areYouSure.gameObject.SetActive(true);

}

public void  No (){
	areYouSure.gameObject.SetActive(false);
}

public void  Yes (){
	Application.Quit();
}

}