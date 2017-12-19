using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMarker : MonoBehaviour {

	// Rotation transform that the minimap marker would be locked at (x only)
	public float m_lockRotX = 90;
	// Rotation transform that the minimap marker would be locked at
	public float m_lockRotYZ = 0;
	// Position transform that the minimap marker would be locked at
	public float m_lockPos = 19f;

	void Update()
	{
		// Rotation along y and z axes is reset to 0 every frame
     	transform.rotation = Quaternion.Euler(m_lockRotX, m_lockRotYZ, m_lockRotYZ);
		// Position along y axis is reset to 19 every frame
		transform.position = new Vector3 (transform.position.x, m_lockPos, transform.position.z);
	}
}
