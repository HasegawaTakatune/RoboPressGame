using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExplosion : MonoBehaviour {
	float waitTime = 0.5f;
	[SerializeField] GameObject[] child = new GameObject[2];
	// Use this for initialization
	void Start () {
		StartCoroutine (Explosion ());
	}

	IEnumerator Explosion(){
		yield return new WaitForSeconds (waitTime);
		Destroy (child[0]);
		Destroy (child[1]);
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
