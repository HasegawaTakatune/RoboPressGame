using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllArm : MonoBehaviour {
	// List
	[SerializeField]
	List<Arm> arms = new List<Arm>();
	// アームのインデックス
	int index = 0;
	// クリック座標
	Vector3 clickPos;
	// ワールド座標
	Vector3 worldPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Mouse ();	
	}

	void Mouse(){
		if (Input.GetMouseButtonUp (0)) {
			arms [index].SetTage (MousePosition ());
			arms [index].SetStatus (1);
			index = (index >= 1) ? 0 : 1;

		}
	}

	Vector3 MousePosition(){
		// クリック座標
		clickPos = Input.mousePosition;
		clickPos.z = 10;
		// ワールド座標に変換
		worldPos = Camera.main.ScreenToWorldPoint (clickPos);
		return worldPos;
	}
}
