﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArm : MonoBehaviour {
	
	// 方向
	private Vector2 angle;
	// 速度
	[SerializeField]
	private float speed=0.1f;
	// 基準位置
	private Vector3 myPos;
	// 移動量
	private Vector3 movePos;
	// ターゲット座標
	public Vector3 targetPos;
	public bool onTarget = false;
	// 当たった敵を格納
	[SerializeField]
	private List<GameObject> hitObj = new List<GameObject>();
	// もくじ
	private int index;

	// Use this for initialization
	void Start () {
		movePos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		// ステータス選択
		switch (RoboStat.status) {
		case RoboStat.Idol:
			break;
		case RoboStat.Targetting:
			TargettingAction ();
			break;
		case RoboStat.Attack:
			AttackAction ();
			break;
		case RoboStat.MoveToTarget:
			MoveToTargettingPoint ();
			break;
		case RoboStat.Delete:
			DeleteAction ();
			break;
		default:
			break;
		}
	}
	// ターゲティング処理
	private void TargettingAction(){
		transform.rotation = Quaternion.Euler (0, 0, angle.y / 3.14f * 180);
	}
	private void MoveToTargettingPoint(){
		if (Mathf.Abs (targetPos.x - transform.position.x) >= 0.5f && Mathf.Abs (targetPos.y - transform.position.y) >= 0.5f) {
			angle.x = Mathf.Atan2 (
				targetPos.x - transform.position.x,
				targetPos.y - transform.position.y
			);
			angle.y = Mathf.Atan2 (
				targetPos.y - transform.position.y,
				targetPos.x - transform.position.x
			);
			transform.rotation = Quaternion.Euler (0, 0, angle.y / 3.14f * 180);
			transform.position += new Vector3 (Mathf.Sin (angle.x) * 1, Mathf.Cos (angle.x) * 1, 0);
		} else
			onTarget = true;
	}

	// 攻撃処理
	private void AttackAction(){
		movePos = new Vector3 (Mathf.Sin (angle.x) * speed, Mathf.Cos (angle.x) * speed, 0);
		transform.position += movePos;
		if (hitObj != null) {
			foreach (GameObject obj in hitObj) {
				obj.transform.position += movePos;
			}
		}
	}
		
	// 終了処理
	private void DeleteAction(){
		index = 0;
		while (true) {
			if (index >= hitObj.Count)break;
			hitObj [index].GetComponent<CharacterStatus> ().ReceiveDamage (10);
			index++;
		}
		hitObj.Clear ();
		if (hitObj.Count == 0)Destroy (this.gameObject);
	}

	// 座標の設定
	public void SetPosition(Vector3 other){
		myPos = other;
	}

	// 座標の取得
	public Vector3 GetPosition(){
		return myPos;
	}

	// 方向の更新
	public void SetTargetPosition(Vector3 other){
		angle.x = Mathf.Atan2 (
			other.x - transform.position.x,
			other.y - transform.position.y
		);
		angle.y = Mathf.Atan2 (
			other.y - transform.position.y,
			other.x - transform.position.x
		);
	}

	// ヒット判定
	void OnTriggerEnter2D (Collider2D other)
	{
		if (RoboStat.status == RoboStat.Attack) {
			if (other.gameObject.tag == "Arm") {
				Touch.RemoveList ();
				RoboStat.status = RoboStat.Idol;
				GetComponent<BoxCollider2D> ().enabled = false;
				RoboStat.status = RoboStat.Delete;
			} else if (other.gameObject.tag == "Enemy") {
				other.gameObject.GetComponent<CharacterStatus> ().ToIdol ();
				hitObj.Add (other.gameObject);
			} else if (other.gameObject.tag == "MotherShip") {
				other.gameObject.GetComponent<CharacterStatus> ().ReceiveDamage (10);
			}
		}
	}

}