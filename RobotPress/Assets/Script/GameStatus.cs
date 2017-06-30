using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour {

	private static int score = 0;
	public static int GetScore(){return score;}
	public static void AddScore(int input){score += input;}

	private static int NumberDestroyed = 0;
	public static int GetNumberDestroyed(){return NumberDestroyed;}
	public static void AddNumberDestroyed(){NumberDestroyed++;}

	public static int flyEnemyDestroyed = 0;
	public static int groundEnemyDestroyed = 0;
	public static int motherShipDestroyed = 0;
	public static int bossDestroyed = 0;

	private static float Hp = 0;
	public static float GetHp(){return Hp;}
	public static void SubtractHp(){Hp--;}

	private static bool updateHp = false;
	public static bool GetUpdateHp(){return updateHp;}
	public static void SetUpdateHp(bool input){updateHp = input;}

	public static void Init(){
		score = 0;
		NumberDestroyed = 0;
		Hp = 40;
		flyEnemyDestroyed = 0;
		groundEnemyDestroyed = 0;
		motherShipDestroyed = 0;
		bossDestroyed = 0;
	}
}