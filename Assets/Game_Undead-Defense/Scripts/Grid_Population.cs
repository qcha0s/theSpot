using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Population : MonoBehaviour {

	public GameObject block;

	void Start() {
		int i = 0;
		int j = 0;
		Vector3 pos = gameObject.transform.position;
		for(i = 0; i < 10; ++i) {
			pos.x = i;
			for(j = 0; j < 10; ++j) {
				pos.z =j;
				Instantiate(block, pos, Quaternion.identity);
			}
		}
	}
}
