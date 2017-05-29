using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	
	// スポーンさせるオブジェクト
	[SerializeField]private GameObject Obj;
	// スポーン座標の振れ幅
	[SerializeField]private Vector2 ShakeWidth = new Vector2(-1.0f,1.5f);
	// 次のスポーンまでの間隔
	private float interval = 0.6f;

	// スポーン
	IEnumerator Spawn(){
		while (true) {
			// 敵のスポーン
			Instantiate (Obj,
				new Vector3 (transform.position.x, transform.position.y + Random.Range (ShakeWidth.x, ShakeWidth.y)), 
				Quaternion.identity);
			yield return new WaitForSeconds (interval);
		}
	}
	// 活動開始
	public void OnActivate(){
		StartCoroutine (Spawn());
	}
}
