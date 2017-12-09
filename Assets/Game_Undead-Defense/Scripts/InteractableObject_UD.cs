using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject_UD : MonoBehaviour {

	public abstract void Interact();
	public abstract void OnBeginRaycast();
	public abstract void OnEndRaycast();
}
