using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMarker : MonoBehaviour {

	// Rotation transform that the minimap marker is locked at
	public float m_lockRot = 0;
	public float m_lockPos = 19f;

	void Update()
	{
		// Rotation along y and z axes is reset to 0 every frame
     	transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, m_lockRot, m_lockRot);
		// Position along y axis is reset to 19 every frame
		transform.position = new Vector3 (transform.position.x, m_lockPos, transform.position.z);
	}
}
