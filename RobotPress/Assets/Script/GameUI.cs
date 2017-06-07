using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	private Text score;
	private static bool setScore = true;

	// Use this for initialization
	void Start () {
		score = GameObject.Find ("Score").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (setScore) {
			score.text = "Score:" + GameStatus.GetScore ().ToString ();
			setScore = false;
		}
	}

	public static void SetScore(){
		setScore = true;
	}
}
