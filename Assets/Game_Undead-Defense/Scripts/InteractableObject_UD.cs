using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractableObject_UD {

	void Interact();
	void OnBeginRaycast();
	void OnEndRaycast();
}
