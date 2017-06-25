using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllArm : MonoBehaviour {
	[SerializeField]
	GameObject Spark;
	// List
	[SerializeField]
	List<Arm> arms = new List<Arm>();
	// アームのインデックス
	int index = 0;
	// クリック座標
	Vector3 clickPos;
	// ワールド座標
	Vector3 worldPos;
	// 長押し時間
	float pressTime = 0;
	bool press = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!InAction ()) {
			Mouse ();
		}
	}

	void Mouse(){
		if (Input.GetMouseButtonUp (0)) {
			if (pressTime < 0.3f) {
				arms [index].SetTage (MousePosition ());
				arms [index].SetStatus (1);
				arms [index].SetAttackStatus (0);
				index = (index >= 1) ? 0 : 1;
			}
		}
		if (Input.GetMouseButton (0)) {
			pressTime += Time.deltaTime;
			if (pressTime >= 0.3f && !press) {
				Vector3 musPos = MousePosition ();
				arms [0].SetTage (musPos + (Vector3.left * 5));
				arms [0].SetStatus (1);
				arms [0].SetAttackStatus (1);
				arms [1].SetTage (musPos + (Vector3.right * 5));
				arms [1].SetStatus (1);
				arms [1].SetAttackStatus (1);
				Instantiate (Spark, musPos, Quaternion.identity);
				press = true;
			}
		} else
			ResetPress ();
	}

	Vector3 MousePosition(){
		// クリック座標
		clickPos = Input.mousePosition;
		clickPos.z = 10;
		// ワールド座標に変換
		worldPos = Camera.main.ScreenToWorldPoint (clickPos);
		return worldPos;
	}

	void ResetPress(){
		press = false;
		pressTime = 0;
	}

	bool InAction(){
		if (arms [0].GetStatus () == 0 || arms [1].GetStatus () == 0)
			return false;

		return true;
	}
}
