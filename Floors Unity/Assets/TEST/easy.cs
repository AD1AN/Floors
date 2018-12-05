using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easy : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			transform.position = new Vector3(transform.position.x, 2, transform.position.z);
			transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 9, ForceMode.Impulse);
		}
	}
}
