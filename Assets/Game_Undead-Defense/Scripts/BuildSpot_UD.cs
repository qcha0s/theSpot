using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpot_UD : MonoBehaviour, InteractableObject_UD {

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

	void InteractableObject_UD.Interact() {
		GameManager_UD.instance.ShowBuildTowerUI(this);
	}

	void InteractableObject_UD.OnBeginRaycast() {
		if (!m_hasTower) {
			m_mesh.material = m_hoveredMat;
		}
	}

	void InteractableObject_UD.OnEndRaycast() {
		m_mesh.material = m_unhoveredMat;
	}

	public void ResetBlock() {
		m_hasTower = false;
		m_floatingImage.SetActive(true);
	}

	public void BuildTower(int tower){
		if (!m_hasTower) {
			m_mesh.material = m_unhoveredMat;
			m_hasTower = true;
			GameObject temp = PoolManager_UD.Instance.GetObject(tower);
			temp.transform.position = m_spawnTransform.position;
			temp.SetActive(true);
			m_floatingImage.SetActive(false);
		}
	}
}
