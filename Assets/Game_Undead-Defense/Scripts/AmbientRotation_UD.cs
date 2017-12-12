using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientRotation_UD : MonoBehaviour {

	public float m_angularSpeedX = 0;
	public float m_angularSpeedY = 360;
	public float m_angularSpeedZ = 0;

	private void Update() {
		Vector3 deltaRotation = new Vector3(m_angularSpeedX,m_angularSpeedY,m_angularSpeedZ);
		deltaRotation *= Time.deltaTime;
		transform.Rotate(deltaRotation);
	}
}
