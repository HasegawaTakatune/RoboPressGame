using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	private bool toTheResult = false;

	// Use this for initialization
	void Start () {
		GameStatus.Init ();
	}
	
	// Update is called once per frame
	void Update () {
		if (toTheResult) {
			StartCoroutine (myFunc.Delay (1, () => {
				SceneManager.LoadScene ("Result");
			}));
			toTheResult = false;
		}
	}
	public void ToTheResult(){
		toTheResult = true;
	}
}
