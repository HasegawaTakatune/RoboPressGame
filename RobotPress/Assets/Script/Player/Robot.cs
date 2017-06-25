using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
	bool operation = false;
	// Use this for initialization
	void Start () {
		StartCoroutine (DuringStartup ());
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += ((operation == true) ? Vector3.up : Vector3.down) * 0.005f;
	}
	IEnumerator DuringStartup(){
		while (true) {
			yield return new WaitForSeconds (0.5f);
			operation = !operation;
		}
	}
}
