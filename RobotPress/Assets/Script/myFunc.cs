using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFunc : MonoBehaviour {
	// 遅延処理
	public static IEnumerator Delay(float waitTime,Action action){
		yield return new WaitForSeconds (waitTime);
		action ();
		yield break;
	}
}
