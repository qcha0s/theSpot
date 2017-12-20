using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenu: MonoBehaviour {


private float sliderValue = 0.0f;
private float sliderValueSFX = 0.0f;
private float sliderValueXSensitivity = 0.0f;
private float sliderValueYSensitivity = 0.0f;
private float sliderValueSmoothing = 0.0f;

void  Start (){

}



void  MusicSlider (){
	PlayerPrefs.SetFloat("MusicVolume", sliderValue);
}

void  SFXSlider (){
	PlayerPrefs.SetFloat("SFXVolume", sliderValueSFX);
}

void  SensitivityXSlider (){
	PlayerPrefs.SetFloat("XSensitivity", sliderValueXSensitivity);
}

void  SensitivityYSlider (){
	PlayerPrefs.SetFloat("YSensitivity", sliderValueYSensitivity);
}

void  SensitivitySmoothing (){
	PlayerPrefs.SetFloat("MouseSmoothing", sliderValueSmoothing);
	Debug.Log(PlayerPrefs.GetFloat("MouseSmoothing"));
}

}