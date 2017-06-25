using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {
	[SerializeField]GameObject child;
	bool expanding = true;
	// キャッチしたオブジェクト
	private List<CharacterStatus> chara = new List<CharacterStatus>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (expanding) {
			StartCoroutine (Expanding ());
			expanding = false;
		}

		// マウスが離されたら
		if (Input.GetMouseButtonUp (0)) {
			// ビルショット攻撃
			if (chara.Count > 0) {
				while(true){
					chara [chara.Count - 1].ActivateShot ();
					chara.Remove (chara [chara.Count - 1]);
					if (chara.Count <= 0)break;
				}
			}
			transform.DetachChildren ();
			Destroy (child);
			Destroy (this.gameObject);
		}

	}

	// 当たり判定
	void OnTriggerEnter2D(Collider2D other){
		GameObject obj = other.gameObject;
		if (obj.tag == "Bill" || obj.tag == "Enemy") {
			// 当たり判定の準備（物理処理付加）
			obj.AddComponent<Rigidbody2D> ();
			obj.GetComponent<Rigidbody2D> ().gravityScale = 0;
			// 
			CharacterStatus chas = obj.GetComponent<CharacterStatus>();
			chas.ToIdol ();
			// 掴んだオブジェクトを保持
			chara.Add(chas);
		}
	}

	private IEnumerator Expanding(){
		while (true) {
			yield return new WaitForSeconds (0.5f);
			transform.localScale += new Vector3 (0.5f, 0.5f, 0);
			if (transform.localScale.x >= 4) {
				yield break;
			}
		}
	}
}
