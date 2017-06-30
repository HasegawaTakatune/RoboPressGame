using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
	[SerializeField]GameObject effect;
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

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			Instantiate (effect, other.gameObject.transform.position, Quaternion.identity);
			Destroy (other.gameObject);
			GameStatus.SubtractHp ();
			GameStatus.SetUpdateHp (false);
		}
	}
}
