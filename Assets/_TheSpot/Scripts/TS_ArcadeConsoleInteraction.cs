using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TS_ArcadeConsoleInteraction : MonoBehaviour, InteractableObject_UD {

	public RectTransform m_hoverMenu;
	public float m_scaleBigTime = 1f;
	public float m_scaleSmallTime = 2f;

	void InteractableObject_UD.Interact() {
		Debug.Log("interacting with " + gameObject.name);
	}

	void InteractableObject_UD.OnBeginRaycast() {
		StartCoroutine(ScaleCanvasBig());
	}

	void InteractableObject_UD.OnEndRaycast() {
		StartCoroutine(ScaleCanvasSmall());
	}

	IEnumerator ScaleCanvasBig() {
		for (float t = 0; t < m_scaleBigTime; t+=Time.deltaTime) {
			float scale = t/m_scaleBigTime;
			m_hoverMenu.localScale = new Vector3(scale,scale,scale);
			yield return null;
		}
	}

	IEnumerator ScaleCanvasSmall() {
		for (float t = m_scaleSmallTime; t > 0; t-=Time.deltaTime) {
			float scale = t/m_scaleSmallTime;
			m_hoverMenu.localScale = new Vector3(scale,scale,scale);
			yield return null;
		}
	}
}
