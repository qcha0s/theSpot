using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_testmove : MonoBehaviour {
	public Rigidbody Cap;
	public float tiltAngle = 100F;
	
	public float moveSpeed = 2f;
	private GameObject bullet;
	
	public GameObject bulletPrefab;
	//public GameObject bullet;
	
    public Transform bulletSpawn;
	public int multiplier = 1;
	private float multiplyTimer = 0;
	private BS_Ultimate m_Ulty;
	// Use this for initialization
	void Start () {
		m_Ulty = GetComponent<BS_Ultimate>();
	}

	IEnumerator Mult() {
		multiplier = 2;
		yield return new WaitForSeconds(30);
        multiplier = 1;
		  
		   
			
        
		
		//5seconds have passed
		Debug.Log(":-)");
	}


	public void Multiply(){
		StartCoroutine(Mult());
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){
			GameObject bullet = (GameObject)Instantiate (bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);
			bullet.GetComponent<BS_Bullet>().ultimate = m_Ulty;
			Destroy(bullet, 2.0f);
				}
		if(Input.GetAxis("Vertical")!=0){
			transform.position += transform.forward * 20 * Input.GetAxis("Vertical") * Time.deltaTime;
		}
		if(Input.GetAxis("Horizontal")!=0){
			
			transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * 40 * Time.deltaTime);
		}
		


   // bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
			
			
		
	}
}

	

