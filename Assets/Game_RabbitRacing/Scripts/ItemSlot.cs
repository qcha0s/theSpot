using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour {
	public Transform m_item;
	public Transform GetItem(){
		return m_item;
	}
	public void SetItem(Transform newItem){
		if(m_item == null){
			m_item = newItem;
		}
	}
	public void UseItem(){
		if(m_item != null){
			Transform item = Instantiate(m_item, transform.position + transform.forward, transform.rotation);
			item.GetComponent<Item>().Use(gameObject);
			m_item = null;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
