
using UnityEngine;
using System.Collections;

public class CheckSFXVolume : MonoBehaviour {


void  Start (){
	// remember volume level from last time
	this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
}

void  UpdateVolume (){
	this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
}
}