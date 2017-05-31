using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	
	// スポーンさせるオブジェクト
	[SerializeField]private GameObject Obj;
	// スポーン座標の振れ幅
	[SerializeField]private Vector2 ShakeWidth = new Vector2(-1.0f,1.5f);
	// 次のスポーンまでの間隔
	[SerializeField]private float interval = 0.6f;
	// ロードした時に始める
	[SerializeField]private bool BeginWhenLoading = false;

	void Start(){
		if (BeginWhenLoading)StartCoroutine (Spawn ());
	}

	// スポーン
	IEnumerator Spawn(){
		float waitTime = interval;
		while (true) {
			// 敵のスポーン
			Instantiate (Obj,
				new Vector3 (transform.position.x, transform.position.y + Random.Range (ShakeWidth.x, ShakeWidth.y)), 
				Quaternion.identity);
			yield return new WaitForSeconds (waitTime);
			waitTime = Random.Range (interval, interval + 2);
		}
	}
	// 活動開始
	public void OnActivate(){
		StartCoroutine (Spawn());
	}
}
