using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpot_UD : InteractableObject_UD {

	public Material m_hoveredMat;
	public GameObject m_arrowTower; //this will be taken out when pool manager and towers are working
	public GameObject m_floatingImage;
	public Transform m_spawnTransform;
	public bool m_hasTower = false;

	private Material m_unhoveredMat;
	private MeshRenderer m_mesh;

	void Awake() {
		m_mesh = GetComponent<MeshRenderer>();
		m_unhoveredMat = m_mesh.material;
	}

	public override void Interact() {
		if (!m_hasTower) {
			m_mesh.material = m_unhoveredMat;
			m_hasTower = true;
			PoolManager_UD.Instance.GetArrowTower(m_spawnTransform.position);
			m_floatingImage.SetActive(false);
		}
	}

	public override void OnBeginRaycast() {
		if (!m_hasTower) {
			m_mesh.material = m_hoveredMat;
		}
	}

	public override void OnEndRaycast() {
		m_mesh.material = m_unhoveredMat;
	}

	public void ResetBlock() {
		m_hasTower = false;
		m_floatingImage.SetActive(true);
	}
}
