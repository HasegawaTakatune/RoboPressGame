using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour {

	private static int score = 5000;
	public static int GetScore(){return score;}
	public static void AddScore(int input){score += input;}

	private static int NumberDestroyed = 0;
	public static int GetNumberDestroyed(){return NumberDestroyed;}
	public static void AddNumberDestroyed(){NumberDestroyed++;}

	public static void Init(){
		score = 0;
		NumberDestroyed = 0;
	}
}