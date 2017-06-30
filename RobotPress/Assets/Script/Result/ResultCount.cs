using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultCount : MonoBehaviour {

	private bool isDestroyedCount = true;
	[SerializeField]private GameObject[] obj;
	private int destroyCount = 0;
	public static float dropTime = 0.5f;

	private bool isScoreCount = true;
	private int count = 0;
	private Text ScoreText;
	private int score = GameStatus.GetScore ();

	// Use this for initialization
	void Start () {
		ScoreText = GameObject.Find ("Score").GetComponent<Text> ();
		StartCoroutine (ScoreCount ());
		StartCoroutine (NumDestroyedCount ());
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDestroyedCount)
		if (Input.GetMouseButtonDown (0))
			SceneManager.LoadScene ("Menu");
		
	}

	private IEnumerator ScoreCount(){
		while (true) {
			yield return new WaitForSeconds (0.001f);
			if (isScoreCount) {
				if (count / 100 <= score / 100)	count += 100;
				else count += 10;
				ScoreText.text = "スコア " + count.ToString ();
				if (count >= score)
					isScoreCount = false;
			} else
				break;
		}
	}

	private IEnumerator NumDestroyedCount(){
		while (true) {
			yield return new WaitForSeconds (0.01f);
			if (isDestroyedCount) {
				//destroyCount++;
				if ((destroyCount % 10) == 0)
					dropTime -= 0.002f;
				
				if (GameStatus.flyEnemyDestroyed > 0) {
					Instantiate (obj [0], new Vector3 (Random.Range (-3.0f, 3.0f), 0, 0), Quaternion.identity);
					GameStatus.flyEnemyDestroyed--;
				} else if (GameStatus.groundEnemyDestroyed > 0) {
					Instantiate (obj [1], new Vector3 (Random.Range (-3.0f, 3.0f), 0, 0), Quaternion.identity);
					GameStatus.groundEnemyDestroyed--;
				} else if (GameStatus.motherShipDestroyed > 0) {
					Instantiate (obj [2], new Vector3 (Random.Range (-3.0f, 3.0f), 0, 0), Quaternion.identity);
					GameStatus.motherShipDestroyed--;
				} else if (GameStatus.bossDestroyed > 0) {
					Instantiate (obj [3], new Vector3 (Random.Range (-3.0f, 3.0f), 0, 0), Quaternion.identity);
					GameStatus.bossDestroyed--;
				} else
					isDestroyedCount = false;
			} else
				break;
		}
	}
}
