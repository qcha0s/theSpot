using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	private BaseKartMovement m_kartMovement;
	private ItemSlot m_itemSlot;
	// Use this for initialization
	void Start () {
		m_kartMovement = GetComponent<BaseKartMovement>();
		m_itemSlot = GetComponent<ItemSlot>();
	}
	
	// Update is called once per frame
	void Update () {
		float forwardMovement = Input.GetAxis("Vertical");
		if(forwardMovement >= 0){
			m_kartMovement.Gas(forwardMovement);
		}else{
			m_kartMovement.Brake(-forwardMovement);
		}
		m_kartMovement.Turn(Input.GetAxis("Horizontal"));

		if(Input.GetButtonDown("Fire1")){
			Debug.Log("FIRE");
			m_itemSlot.UseItem();
		}
	}
}
