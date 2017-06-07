using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour {

	private static int score;
	public static int GetScore(){return score;}
	public static void AddScore(int input){score += input;}


	public static void Init(){
		score = 0;
	}
}
