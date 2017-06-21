using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchArms : MonoBehaviour {
	// キャッチオブジェクトの保持
	private List<CharacterStatus> catchObj = new List<CharacterStatus>();
	//private CharacterStatus[] catchObj;
	//  子オブジェクトのアームを保持
	private RobotCatchArms[] roboCArms =  new RobotCatchArms[2];
	// マウス移動の制御
	private bool mouseMove = false;
	// マウス座標
	private Vector3 mousePos;
	// ワールド座標
	private Vector3 worldPos;
	// 
	public static bool expanding = false;

	// Use this for initialization
	void Start () {
		roboCArms = GetComponentsInChildren<RobotCatchArms> ();
	}
	
	// Update is called once per frame
	void Update () {
		// マウスが離されたら
		if (Input.GetMouseButtonUp (0)) {
			// ビルショット攻撃
			if (catchObj.Count > 0) {
				while(true){
					catchObj [catchObj.Count - 1].ActivateShot ();
					catchObj.Remove (catchObj [catchObj.Count - 1]);
					if (catchObj.Count <= 0)break;
				}
			}
			// 子オブジェクトを削除する
			for (int i = 0; i < 2; i++) {
				roboCArms [i].CallDestroy ();
			}
			transform.DetachChildren ();
			Destroy (this.gameObject);
		}
	}
	// 親子関係の設定
	public void ParentSetting(GameObject obj){
		obj.AddComponent<Rigidbody2D> ();
		obj.GetComponent<Rigidbody2D> ().gravityScale = 0;
		CharacterStatus chas = obj.GetComponent<CharacterStatus> ();
		chas.ToIdol();
		// 掴んだオブジェクトを保持
		catchObj.Add(chas);
		obj.transform.parent = transform;
	}
}
