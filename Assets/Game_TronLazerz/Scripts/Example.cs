using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {

	private Material m_mat; 


	public Color colorStart = Color.red;
    public Color colorEnd = Color.green;
    public float duration = 1.0F;
    public Renderer rend;
	private Color newcolor; 
    void Start() {
        rend = GetComponent<SpriteRenderer>();
		
		newcolor.r = 0f;
		newcolor.g = 0f;
		newcolor.b = 255f;
		
    }
    void Update() {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.color = new Color(0f, 0f, 255f, 1f); // Set to opaque black
    }

	// Use this for initialization
	// void Start () {
	// 	m_mat = GetComponent<Material>();	
	// }
	
	// // Update is called once per frame
	// void Update () {
	// 	m_mat.color = 
	// }
}
