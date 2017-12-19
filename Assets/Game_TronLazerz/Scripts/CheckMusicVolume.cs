// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class CheckMusicVolume : MonoBehaviour {


void  Start (){
	// remember volume level from last time
	this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
}

void  UpdateVolume (){
	this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
}
}