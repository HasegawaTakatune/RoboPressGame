using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCount : MonoBehaviour {

	private bool isDestroyedCount = true;
	[SerializeField]private GameObject[] obj;
	private int NumDestroyed = GameStatus.GetNumberDestroyed ();
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
		
	}

	private IEnumerator ScoreCount(){
		while (true) {
			yield return new WaitForSeconds (0.001f);
			if (isScoreCount) {
				count++;
				ScoreText.text = "Score " + count.ToString ();
				if (count >= score)isScoreCount = false;
			}
		}
	}

	private IEnumerator NumDestroyedCount(){
		while (true) {
			yield return new WaitForSeconds (0.01f);
			if (isDestroyedCount) {
				destroyCount++;
				if((destroyCount%10) == 0) dropTime -= 0.01f;
				Instantiate (obj [0], new Vector3 (Random.Range (-3.0f, 3.0f), 0, 0), Quaternion.identity);
				if (destroyCount >= NumDestroyed)isDestroyedCount = false;
			}
		}
	}
}
