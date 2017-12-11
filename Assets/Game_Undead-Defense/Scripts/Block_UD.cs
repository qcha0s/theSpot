using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_UD : InteractableObject_UD {

	public Material hovered_Color;

	public Material unhovered_Color;

	public Material clicked_Color;

	public GameObject Tower;

	bool isClicked = false;
	private MeshRenderer current_Color;

	void Awake() {
		current_Color = GetComponent<MeshRenderer>();
	}

	void Update() {

	}

	public override void Interact() {
		Vector3 offset = Vector3.zero;
		offset = gameObject.transform.position;
		offset.y = gameObject.transform.position.y + 1;
		current_Color.material = clicked_Color;
		isClicked = true;
		Instantiate(Tower, offset, Quaternion.identity);
	}

	public override void OnBeginRaycast() {
		current_Color.material = hovered_Color;
		if(isClicked) {
			current_Color.material = clicked_Color;
		}
	}

	public override void OnEndRaycast() {
		current_Color.material = unhovered_Color;
		if(isClicked) {
			current_Color.material = clicked_Color;
		}
	}
	




}
