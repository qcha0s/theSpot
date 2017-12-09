using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour {
	private Transform m_item;
	public Transform GetItem(){
		return m_item;
	}
	public void SetItem(Transform newItem){
		if(m_item == null){
			m_item = newItem;
		}
	}
	public void UseItem(){
		Instantiate(m_item);
		m_item.GetComponent<Item>().Use(gameObject);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
