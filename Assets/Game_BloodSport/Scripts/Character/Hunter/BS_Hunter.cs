using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Hunter : MonoBehaviour {
	
	public GameObject m_Arrow;
	public GameObject m_ArrowSpawn;
	GameObject m_ArrowHandler;
	Rigidbody m_ArrowRB;

	public GameObject m_Ultimate;
	public GameObject m_IceArrow;
	public GameObject m_FlameArrow;

	private float m_ArrowVelocity = 500;





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("Fire1")) {
			ArrowFire();	
		}
		if(Input.GetButtonDown("Alpha01")) {
			IceArrow();
		}
		if(Input.GetButtonDown("Alpha02")) {
			FlameArrow();
		}
		if(Input.GetButtonDown("Alpha03")) {
			Ultimate();
		}
	}

	void ArrowFire() {

		m_ArrowHandler = GameObject.Instantiate(m_Arrow, m_ArrowSpawn.transform.position, m_ArrowSpawn.transform.rotation);
		m_ArrowHandler.transform.Rotate(Vector3.left * 90);

		m_ArrowRB = m_ArrowHandler.GetComponent<Rigidbody>();
		m_ArrowRB.AddForce(transform.forward * m_ArrowVelocity);

		Destroy(m_ArrowHandler, 10.0f);



	}

	void IceArrow() {

	}

	void FlameArrow() {

	}

	void Ultimate() {

	}


}
