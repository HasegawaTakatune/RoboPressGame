using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	private bool toTheResult = false;
	public GameObject boss;
	bool doOnce = false;

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
		if (!doOnce && GameStatus.GetScore() >= 300) {
			boss.SetActive (true);
			doOnce = true;
		}
	}
	public void ToTheResult(){
		toTheResult = true;
	}
}
