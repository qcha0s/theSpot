using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//resize the minimap to a square

public class Minimap_UD : MonoBehaviour {

	Camera m_minimapCamera;

	void Awake(){
		m_minimapCamera = GetComponent<Camera>();

		float height = m_minimapCamera.pixelRect.height;

		Rect pixelRect = m_minimapCamera.pixelRect;

		pixelRect.width = height;
		pixelRect.x = (float)Screen.width - height;

		m_minimapCamera.pixelRect = pixelRect;
		Debug.Log(m_minimapCamera.pixelRect);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
