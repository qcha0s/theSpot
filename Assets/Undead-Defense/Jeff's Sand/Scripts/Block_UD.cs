using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_UD : MonoBehaviour {

	public Material hovered_Color;

	public Material unhovered_Color;

	public Material clicked_Color;

	bool isClicked = false;
	private MeshRenderer current_Color;

	void Awake() {
		current_Color = GetComponent<MeshRenderer>();
	}

	void Update() {

	}

	void OnMouseDown() {
		current_Color.material = clicked_Color;
		isClicked = true;
	}
	void OnMouseOver() {
		current_Color.material = hovered_Color;
		if(isClicked) {
			current_Color.material = clicked_Color;
		}
	}

	void OnMouseExit() {
		current_Color.material = unhovered_Color;
		if(isClicked) {
			current_Color.material = clicked_Color;
		}
	}
	




}
