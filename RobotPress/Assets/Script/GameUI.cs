using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	private Text score;
	private static bool setScore = true;

	private LineRenderer lineRenderer;
	private bool dead = false;

	private Text gameOver;

	// Use this for initialization
	void Start () {
		score = GameObject.Find ("Score").GetComponent<Text> ();
		lineRenderer = GameObject.Find ("Hp").GetComponent<LineRenderer> ();
		gameOver = GameObject.Find ("GameOver").GetComponent<Text> ();
		// 線の幅
		lineRenderer.SetWidth (0.2f, 0.2f);
		// 頂点の数
		lineRenderer.SetVertexCount(2);
		// 頂点を設定
		lineRenderer.SetPosition(0,new Vector3(-7.5f,4.5f,0));
		lineRenderer.SetPosition (1, new Vector3 ((GameStatus.GetHp() / 4) - 7.5f, 4.5f, 0));
	}
	
	// Update is called once per frame
	void Update () {
		if (setScore) {
			score.text = "スコア:" + GameStatus.GetScore ().ToString ();
			setScore = false;
		}
		if (!GameStatus.GetUpdateHp () && !dead) {
			lineRenderer.SetPosition (1, new Vector3 ((GameStatus.GetHp()/4) - 7.5f, 4.5f, 0));
			GameStatus.SetUpdateHp (true);
			if (GameStatus.GetHp () <= 0)
				dead = true;
		}
		if (dead) {
			gameOver.text = "GAME OVER";
			StartCoroutine (Result());
		}
	}

	public static void SetScore(){
		setScore = true;
	}

	IEnumerator Result(){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene ("Result");
	}
}
