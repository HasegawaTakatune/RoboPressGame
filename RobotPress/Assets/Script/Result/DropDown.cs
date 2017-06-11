using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour {

	private bool drop = true;

	// Use this for initialization
	void Start () {
		StartCoroutine (myFunc.Delay (ResultCount.dropTime, () => {
			drop = false;
		}));
	}
	
	// Update is called once per frame
	void Update () {
		if (drop)
			transform.position -= new Vector3 (0, 0.1f, 0);
	}
}
