
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {



public Animator CameraObject;
public  GameObject PanelControls;
//public GameObject PanelVideo;
public GameObject PanelGame;
//public GameObject PanelKeyBindings;
public GameObject PanelMovement;
public GameObject PanelCombat;
public GameObject PanelGeneral;
public GameObject hoverSound;
public GameObject sfxhoversound;
public GameObject clickSound;
public GameObject areYouSure;

// campaign button sub menu
// public GameObject continueBtn;
// public GameObject newGameBtn;
// public GameObject loadGameBtn;

// highlights
public GameObject lineGame;
// public GameObject lineVideo;
public GameObject lineControls;
// public GameObject lineKeyBindings;
public GameObject lineMovement;
public GameObject lineCombat;
public GameObject lineGeneral;

// public void  PlayCampaign (){
// 	areYouSure.gameObject.SetActive(false);
// //	continueBtn.gameObject.SetActive(true);
// 	newGameBtn.gameObject.SetActive(true);
// 	loadGameBtn.gameObject.SetActive(true);
	
// }
public void StartGameBtn(string sceneName){
        
        SceneManager.LoadScene(sceneName);
    }


// public void  DisablePlayCampaign (){
// 	continueBtn.gameObject.SetActive(false);
// 	newGameBtn.gameObject.SetActive(false);
// 	loadGameBtn.gameObject.SetActive(false);
// }

public void  Position2 (){
	//DisablePlayCampaign();
	CameraObject.SetFloat("Animate",1);
}

public void  Position1 (){
	CameraObject.SetFloat("Animate",0);
}
public void  Position3 (){
	CameraObject.SetFloat("Animate",2);
}

public void  GamePanel (){
	PanelControls.gameObject.SetActive(false);
	// PanelVideo.gameObject.SetActive(false);
	// PanelGame.gameObject.SetActive(true);
	// PanelKeyBindings.gameObject.SetActive(false);

	lineGame.gameObject.SetActive(true);
	lineControls.gameObject.SetActive(false);
	// lineVideo.gameObject.SetActive(true);
	// lineKeyBindings.gameObject.SetActive(false);
}

public void  VideoPanel (){
	PanelControls.gameObject.SetActive(false);
	// PanelVideo.gameObject.SetActive(true);
	// PanelGame.gameObject.SetActive(false);
	// PanelKeyBindings.gameObject.SetActive(false);

	lineGame.gameObject.SetActive(false);
	lineControls.gameObject.SetActive(false);
	// lineVideo.gameObject.SetActive(true);
	// lineKeyBindings.gameObject.SetActive(false);
}

public void  ControlsPanel (){
	PanelControls.gameObject.SetActive(true);
	// PanelVideo.gameObject.SetActive(false);
	// PanelGame.gameObject.SetActive(false);
	// PanelKeyBindings.gameObject.SetActive(false);

	lineGame.gameObject.SetActive(false);
	lineControls.gameObject.SetActive(true);
	// lineVideo.gameObject.SetActive(false);
	// lineKeyBindings.gameObject.SetActive(false);
}

public void  KeyBindingsPanel (){
	PanelControls.gameObject.SetActive(false);
	// PanelVideo.gameObject.SetActive(false);
	// PanelGame.gameObject.SetActive(false);
	// PanelKeyBindings.gameObject.SetActive(true);
	lineGame.gameObject.SetActive(false);
	lineControls.gameObject.SetActive(false);
	// lineVideo.gameObject.SetActive(true);
	// lineKeyBindings.gameObject.SetActive(true);
}

public void  MovementPanel (){
	PanelMovement.gameObject.SetActive(true);
	PanelCombat.gameObject.SetActive(false);
	PanelGeneral.gameObject.SetActive(false);

	lineMovement.gameObject.SetActive(true);
	lineCombat.gameObject.SetActive(false);
	lineGeneral.gameObject.SetActive(false);
}

public void  CombatPanel (){
	PanelMovement.gameObject.SetActive(false);
	PanelCombat.gameObject.SetActive(true);
	PanelGeneral.gameObject.SetActive(false);

	lineMovement.gameObject.SetActive(false);
	lineCombat.gameObject.SetActive(true);
	lineGeneral.gameObject.SetActive(false);
}

public void  GeneralPanel (){
	PanelMovement.gameObject.SetActive(false);
	PanelCombat.gameObject.SetActive(false);
	PanelGeneral.gameObject.SetActive(true);

	lineMovement.gameObject.SetActive(false);
	lineCombat.gameObject.SetActive(false);
	lineGeneral.gameObject.SetActive(true);
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
//	DisablePlayCampaign();
}

public void  No (){
	areYouSure.gameObject.SetActive(false);
}

public void  Yes (){
	Application.Quit();
}

}