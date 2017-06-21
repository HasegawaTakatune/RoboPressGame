using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Touch : MonoBehaviour {
	// アームの種類
	private const byte NormalArm = 0,CatchArm = 1;
	// ロボットアーム
	[SerializeField]
	private GameObject spawnArm;
	// キャッチアーム
	[SerializeField]
	private GameObject catchArms;
	// List
	[SerializeField]
	static List<RobotArm> robotArms = new List<RobotArm>();
	// 長押し時間
	float pressTime;
	// 長押し判定
	bool press = false;

	// クリック座標
	Vector3 clickPos;
	// ワールド座標
	Vector3 worldPos;

	// Update is called once per frame
	void Update () {
		//MouseTouch ();
		MouseClick ();
	}

	// マウスイベント処理
	void MouseClick(){

		// 左長押ししたら
		if(Input.GetMouseButton(0) && robotArms.Count < 2){
			pressTime += Time.deltaTime;
			if (pressTime > 0.3f && !press) {
				ClickSpawn (CatchArm);
				press = true;
			}
		}
		// 左クリックした時
		if (Input.GetMouseButtonUp (0)) {
			if (pressTime < 1.0f && robotArms.Count < 2) {
				ClickSpawn (NormalArm);
			}
			pressTime = 0;
			press = false;
		}
		// 位置設定
		if (robotArms.Count > 1) {
			robotArms [0].SetTargetPosition (robotArms [1].GetPosition ());
			robotArms [1].SetTargetPosition (robotArms [0].GetPosition ());
			// ステータス変更
			switch (RoboStat.status) {
			case RoboStat.Idol:
				// 方向修正
				RoboStat.status = RoboStat.Targetting;
				break;
			case RoboStat.Targetting:
				if (press) {
					// つかむ
					StartCoroutine (Delay (0.1f, () => {
						RoboStat.status = RoboStat.Catch;
					}));
				} else {
					// 移動
					StartCoroutine (Delay (0.1f, () => {
						RoboStat.status = RoboStat.Attack;
					}));
				}
				break;
			case RoboStat.MoveToTarget:
				if(robotArms[0].onTarget&&robotArms[1].onTarget){
					RoboStat.status = RoboStat.Targetting;
				}
				break;
			}
		}
	}

	// 遅延処理
	IEnumerator Delay(float waitTime,Action action){
		yield return new WaitForSeconds (waitTime);
		action ();
		yield break;
	}

	// リストの全削除
	public static void RemoveList(){
		robotArms.Clear ();
	}

	// クリック時のアクション
	void ClickSpawn(byte spawnArmNumber){
		// ステータス初期化
		RoboStat.status = RoboStat.Idol;
		// クリック座標
		clickPos = Input.mousePosition;
		clickPos.z = 10;
		// ワールド座標に変換
		worldPos = Camera.main.ScreenToWorldPoint (clickPos);
		// スポーン
		switch(spawnArmNumber){
		case NormalArm:
			RoboStat.status = RoboStat.MoveToTarget;
			// リストに追加
			robotArms.Add (Instantiate (spawnArm, new Vector3(10,10,0), Quaternion.identity).gameObject.GetComponent<RobotArm> ());
			robotArms [robotArms.Count - 1].SetPosition (worldPos);
			robotArms [robotArms.Count - 1].targetPos = worldPos;
			break;
		case CatchArm:
			Instantiate (catchArms, worldPos, Quaternion.identity);
			break;
		}
	}


}
