// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


public  Animator CameraObject;
GameObject PanelControls;
GameObject PanelVideo;
GameObject PanelGame;
GameObject PanelKeyBindings;
GameObject PanelMovement;
GameObject PanelCombat;
GameObject PanelGeneral;
GameObject hoverSound;
GameObject sfxhoversound;
GameObject clickSound;
GameObject areYouSure;

// campaign button sub menu
GameObject continueBtn;
GameObject newGameBtn;
GameObject loadGameBtn;

// highlights
GameObject lineGame;
GameObject lineVideo;
GameObject lineControls;
GameObject lineKeyBindings;
GameObject lineMovement;
GameObject lineCombat;
GameObject lineGeneral;

void  PlayCampaign (){
	areYouSure.gameObject.SetActive(false);
	continueBtn.gameObject.SetActive(true);
	newGameBtn.gameObject.SetActive(true);
	loadGameBtn.gameObject.SetActive(true);
}

void  DisablePlayCampaign (){
	continueBtn.gameObject.SetActive(false);
	newGameBtn.gameObject.SetActive(false);
	loadGameBtn.gameObject.SetActive(false);
}

void  Position2 (){
	DisablePlayCampaign();
	CameraObject.SetFloat("Animate",1);
}

void  Position1 (){
	CameraObject.SetFloat("Animate",0);
}

void  GamePanel (){
	PanelControls.gameObject.SetActive(false);
	PanelVideo.gameObject.SetActive(false);
	PanelGame.gameObject.SetActive(true);
	PanelKeyBindings.gameObject.SetActive(false);

	lineGame.gameObject.SetActive(true);
	lineControls.gameObject.SetActive(false);
	lineVideo.gameObject.SetActive(false);
	lineKeyBindings.gameObject.SetActive(false);
}

void  VideoPanel (){
	PanelControls.gameObject.SetActive(false);
	PanelVideo.gameObject.SetActive(true);
	PanelGame.gameObject.SetActive(false);
	PanelKeyBindings.gameObject.SetActive(false);

	lineGame.gameObject.SetActive(false);
	lineControls.gameObject.SetActive(false);
	lineVideo.gameObject.SetActive(true);
	lineKeyBindings.gameObject.SetActive(false);
}

void  ControlsPanel (){
	PanelControls.gameObject.SetActive(true);
	PanelVideo.gameObject.SetActive(false);
	PanelGame.gameObject.SetActive(false);
	PanelKeyBindings.gameObject.SetActive(false);

	lineGame.gameObject.SetActive(false);
	lineControls.gameObject.SetActive(true);
	lineVideo.gameObject.SetActive(false);
	lineKeyBindings.gameObject.SetActive(false);
}

void  KeyBindingsPanel (){
	PanelControls.gameObject.SetActive(false);
	PanelVideo.gameObject.SetActive(false);
	PanelGame.gameObject.SetActive(false);
	PanelKeyBindings.gameObject.SetActive(true);
	lineGame.gameObject.SetActive(false);
	lineControls.gameObject.SetActive(false);
	lineVideo.gameObject.SetActive(true);
	lineKeyBindings.gameObject.SetActive(true);
}

void  MovementPanel (){
	PanelMovement.gameObject.SetActive(true);
	PanelCombat.gameObject.SetActive(false);
	PanelGeneral.gameObject.SetActive(false);

	lineMovement.gameObject.SetActive(true);
	lineCombat.gameObject.SetActive(false);
	lineGeneral.gameObject.SetActive(false);
}

void  CombatPanel (){
	PanelMovement.gameObject.SetActive(false);
	PanelCombat.gameObject.SetActive(true);
	PanelGeneral.gameObject.SetActive(false);
	lineMovement.gameObject.SetActive(false);
	lineCombat.gameObject.SetActive(true);
	lineGeneral.gameObject.SetActive(false);
}

void  GeneralPanel (){
	PanelMovement.gameObject.SetActive(false);
	PanelCombat.gameObject.SetActive(false);
	PanelGeneral.gameObject.SetActive(true);

	lineMovement.gameObject.SetActive(false);
	lineCombat.gameObject.SetActive(false);
	lineGeneral.gameObject.SetActive(true);
}

void  PlayHover (){
	hoverSound.GetComponent<AudioSource>().Play();
}

void  PlaySFXHover (){
	sfxhoversound.GetComponent<AudioSource>().Play();
}

void  PlayClick (){
	clickSound.GetComponent<AudioSource>().Play();
}

void  AreYouSure (){
	areYouSure.gameObject.SetActive(true);
	DisablePlayCampaign();
}

void  No (){
	areYouSure.gameObject.SetActive(false);
}

void  Yes (){
	Application.Quit();
}

}