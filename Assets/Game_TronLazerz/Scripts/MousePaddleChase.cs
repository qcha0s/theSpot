using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MousePaddleChase : MonoBehaviour {

    public Rigidbody target;
    public float speed;

    void Start(){
        target =GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}

