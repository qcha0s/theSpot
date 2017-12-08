using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (EZReplayManager))]
public class EZReplayManagerEditor : PropertyEditor {
	
	private SerializedProperty EZRiconProperty;
	private SerializedProperty playIconProperty;
	private SerializedProperty pauseIconProperty;
	private SerializedProperty startRecordIconProperty;
	private SerializedProperty stopRecordIconProperty;
	private SerializedProperty replayIconProperty;
	private SerializedProperty stopIconProperty;
	private SerializedProperty closeIconProperty;
	private SerializedProperty rewindIconProperty;
	
	private SerializedProperty showHintsProperty;
	private SerializedProperty useRecordingGUIProperty;
	private SerializedProperty useReplayGUIProperty;
	private SerializedProperty useDarkStripesInReplayProperty;
	private SerializedProperty precacheGameobjectsProperty;
	
	private SerializedProperty compressSavesProperty;
	
	private SerializedProperty sendCallbacksProperty;
	private SerializedProperty callbacksToExecuteProperty;
	private SerializedProperty showFullVersionLicensingInfoProperty;
	private SerializedProperty autoDeactivateLiveObjectsOnReplayProperty;
	private SerializedProperty recordingIntervalProperty;
	private SerializedProperty gameObjectsToRecordProperty;
	private SerializedProperty componentsAndScriptsToKeepAtReplayProperty;
	
	
	protected override void Initialize ()
	{
		EZRiconProperty = serializedObject.FindProperty ("EZRicon");
		playIconProperty = serializedObject.FindProperty ("playIcon");
		pauseIconProperty = serializedObject.FindProperty ("pauseIcon");
		startRecordIconProperty = serializedObject.FindProperty ("startRecordIcon");
		stopRecordIconProperty = serializedObject.FindProperty ("stopRecordIcon");
		replayIconProperty = serializedObject.FindProperty ("replayIcon");
		stopIconProperty = serializedObject.FindProperty ("stopIcon");
		closeIconProperty = serializedObject.FindProperty ("closeIcon");
		rewindIconProperty = serializedObject.FindProperty ("rewindIcon");
		
		showHintsProperty = serializedObject.FindProperty ("showHintsForImportantOptions");
		useRecordingGUIProperty = serializedObject.FindProperty ("useRecordingGUI");
		useReplayGUIProperty = serializedObject.FindProperty ("useReplayGUI");
		useDarkStripesInReplayProperty = serializedObject.FindProperty ("useDarkStripesInReplay");
		precacheGameobjectsProperty = serializedObject.FindProperty ("precacheGameobjects");
		
		compressSavesProperty = serializedObject.FindProperty ("compressSaves");
		
		sendCallbacksProperty = serializedObject.FindProperty ("sendCallbacks");
		callbacksToExecuteProperty = serializedObject.FindProperty ("callbacksToExecute");
		showFullVersionLicensingInfoProperty = serializedObject.FindProperty ("showFullVersionLicensingInfo");
		autoDeactivateLiveObjectsOnReplayProperty = serializedObject.FindProperty ("autoDeactivateLiveObjectsOnReplay");
		recordingIntervalProperty = serializedObject.FindProperty ("recordingInterval");
		gameObjectsToRecordProperty = serializedObject.FindProperty ("gameObjectsToRecord");
		componentsAndScriptsToKeepAtReplayProperty = serializedObject.FindProperty ("componentsAndScriptsToKeepAtReplay");
		
	}
	
	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(Screen.width/1.7f);		
		
		BeginEdit ();
			BeginSection ("Icons and Images");
				PropertyField ("EZR icon", EZRiconProperty);
				PropertyField ("Play icon", playIconProperty);
				PropertyField ("Pause icon", pauseIconProperty);
				PropertyField ("Start recording icon", startRecordIconProperty);
				PropertyField ("Stop recording icon", stopRecordIconProperty);
				PropertyField ("Replay icon", replayIconProperty);
				PropertyField ("Stop icon", stopIconProperty);
				PropertyField ("Close icon", closeIconProperty);
				PropertyField ("Rewind icon", rewindIconProperty);
			EndSection ();
	
			BeginSection ("Options and Features");
				PropertyField ("Show hints/documentation here", showHintsProperty);
				PropertyField ("Use standard recording GUI", useRecordingGUIProperty);
				PropertyField ("Use standard replaying GUI", useReplayGUIProperty);
				PropertyField ("Dark stripes in replaying GUI", useDarkStripesInReplayProperty);
		
				if (showFullVersionLicensingInfoProperty != null) 
					PropertyField ("Show licensing info", showFullVersionLicensingInfoProperty);		
				
				PropertyField ("Precache GameObjects", precacheGameobjectsProperty);
		
				if (EZReplayManager.get.precacheGameobjects) {
					PropertyField ("Compress Saves", compressSavesProperty);			
					Comment ("Compress Saves:\r\n\r\nUse LZF compression to save files.\r\n\r\nDefault: enabled");			
				}
				
				if (EZReplayManager.get.showHintsForImportantOptions)
					Comment ("Precache GameObjects:\r\n\r\nThis option MUST be enabled in order to save replays to a file, also if you are using more complex scene setups in which i.e. children of gameobjects are being removed.\r\n\r\n" +
						"When you enable this option for the very first time in a scene and you start the scene, it can take quite some time to precache all objects. There is also a known bug causing a \"Error moving file\" message from Unity. Don't worry about it, just click \"Try again\". " +
						"The bug is known to be caused by i.e. Avira AntiVirus live-scanner. Second time you launch the scene the precaching doesn't happen anymore.\r\n\r\nWhen you use this option it is important to let the scene play through at least once, to make sure that when the scene is exported, all neccessary gameobjects are precached and available.\r\n\r\n" +
						"If you want to delete the current cache, just remove directory Assets/Resources/EZReplayManagerAssetPrefabs\r\n\r\nDefault: enabled");
		
				PropertyField ("Send callbacks", sendCallbacksProperty);
				if (EZReplayManager.get.sendCallbacks) 
					ArrayPropertyField(callbacksToExecuteProperty);	
				if (EZReplayManager.get.showHintsForImportantOptions)
					Comment ("sendCallbacks:\r\n\r\nYou can let EZReplayManager call a callback function in custom scripts. I.e. in a script define function\r\n\r\npublic void __EZR_record() {\r\n\r\n}\r\n\r\nIf this option is enabled this function will be called when recording starts.\r\n\r\nNote that when you have a lot of gameobjects in the scene this can make it a little slower. In order for callbacks to be successfully executed, they have to be included in the list above. For all implemented callback functions please refer to the documentation.\r\n\r\nDefault: disabled");
		
				PropertyField ("Auto deactivate objects on replay", autoDeactivateLiveObjectsOnReplayProperty);
				if (EZReplayManager.get.showHintsForImportantOptions)
					Comment ("Auto deactivate objects on replay:\r\n\r\nDecide here whether you want the real gameobjects disabled, once the replay starts.\r\n\r\nDefault: enabled");		
		
				PropertyField ("Recording interval (1/FPS)", recordingIntervalProperty);
				if (EZReplayManager.get.showHintsForImportantOptions)
					Comment ("Recording interval:\r\n\r\nThis is the interval in seconds between to frames. It is the equivalent of 1/FPS, FPS being frames per second, a lower value means a higher number of FPS. A value too low will result in unneccessary frames being recorded. A value too high will make the replay seem bumpy.\r\n\r\nDefault: 0.05");		
		
				ArrayPropertyField(gameObjectsToRecordProperty);
				if (EZReplayManager.get.showHintsForImportantOptions)
					Comment ("GameObjectsToRecord:\r\n\r\nThis is the list of preconfigured gameobjects to mark for recording. When this prefab is instantiated in the scene, just drag gameobjects onto this list to have them added to the list and marked for recording automatically without the need for further coding.\r\n\r\nInstead of adding them to the list, you can also mark gameobjects for recording by code, typically in the Start()-function:\r\n\r\nEZReplayManager.mark4Recording(gameObject);");		
				
				ArrayPropertyField(componentsAndScriptsToKeepAtReplayProperty);	
				if (EZReplayManager.get.showHintsForImportantOptions)
					Comment ("ComponentsAndScriptsToKeepAtReplay:\r\n\r\nIf you want to preserve certain scripts on gameobjects in replay mode (i.e. to be able to add visual aide of some sort), please add the name of the script to this list, to have them excluded from scripts to erase on gameobject clones.");			
		
			EndSection ();
		EndEdit ();
	}
}
