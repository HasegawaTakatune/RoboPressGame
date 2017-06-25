using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {
	// 片方の腕
	[SerializeField]GameObject armG;
	// ジェット
	[SerializeField]GameObject EfExplosion;
	Arm armA;
	// ベース座標
	Vector3 basePos;
	// ターゲット座標
	Vector3 tagePos;
	// 到着
	bool arrival = false;
	// ステータス
	[SerializeField]byte status = 0;
	const byte
	Idol = 0,Target = 1,Arrival = 2,Attack = 3,Return = 5;
	byte attackStatus = 0;
	const byte Normal = 0,Spark = 1,None = 9;
	// 止まる
	bool stop = false;
	// 角度
	Vector2 angle;
	// 速度
	float speed = 0.5f;
	// 当たったオブジェクトを保存
	[SerializeField]List<GameObject> hitObj = new List<GameObject>();

	// Use this for initialization
	void Start () {
		armA = armG.GetComponent<Arm> ();
		basePos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		switch (status) {
		case Idol:
			break;
		case Target:
			MoveToTarget ();
			break;
		case Arrival:
			LookPartner ();
			break;
		case Attack:
			if (Attack == armA.GetStatus ()) {
				switch(attackStatus){
				case Normal:
					PressAttack ();
					break;
				case Spark:
					SparkAttack ();
					break;
				default:
					break;
				}
			}
			break;
		case Return:
			ReturnBasePosition ();
			break;
		}
	}

	// ターゲット移動
	void MoveToTarget(){
		if (!arrival) {
			SetAngle ();
			// 向き
			transform.rotation = Quaternion.Euler (0, 0, angle.y / 3.14f * 180);
			// 移動
			transform.position += new Vector3 (Mathf.Sin (angle.x) * speed, Mathf.Cos (angle.x) * speed, 0);
			// 目的地まで着いたか
			if (Mathf.Abs (tagePos.x - transform.position.x) <= 0.5f &&
			    Mathf.Abs (tagePos.y - transform.position.y) <= 0.5f) {
				arrival = true;
				status = Arrival;
			}
		}
		EfExplosion.SetActive (true);
	}
	// パートナーの方向に向く
	void LookPartner(){
		status = Attack;
	}
	// プレス攻撃
	void PressAttack(){
		Vector3 movement = new Vector3 (Mathf.Sin (angle.x) * 0.1f, Mathf.Cos (angle.x) * 0.1f, 0);
		int i = 0;
		SetTargetAngle ();
		// 向き
		transform.rotation = Quaternion.Euler (0, 0, angle.y / 3.14f * 180);
		// 移動
		transform.position += movement;
		while(true){
			if (hitObj.Count <= i)
				break;
			hitObj [i].transform.position += movement;
			i++;
		}
		// 目的地まで着いたか
		if (Mathf.Abs (tagePos.x - transform.position.x) <= 2f &&
		    Mathf.Abs (tagePos.y - transform.position.y) <= 2f) {
			status = Return;
			armA.SetStatus (Return);
		}
	}
	// スパーク攻撃
	void SparkAttack(){
		if (!stop) {
			SetTargetAngle ();
			// 向き
			transform.rotation = Quaternion.Euler (0, 0, angle.y / 3.14f * 180);
			// 移動
			transform.position += new Vector3 (Mathf.Sin (angle.x) * 0.1f, Mathf.Cos (angle.x) * 0.1f, 0);
		}
		// 目的地まで着いたか
		if (Mathf.Abs (tagePos.x - transform.position.x) <= 4f &&
		    Mathf.Abs (tagePos.y - transform.position.y) <= 4f) {
			stop = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			status = Return;
			armA.SetStatus (Return);
		}
	}

	void ReturnBasePosition(){
		int i = 0;
		while (true) {
			if (hitObj.Count <= i || hitObj[i] == null)
				break;
			hitObj [i].GetComponent<CharacterStatus> ().ReceiveDamage (10);
			i++;
		}
		hitObj.Clear ();
		tagePos = basePos;
		SetAngle ();
		// 向き
		transform.rotation = Quaternion.Euler (0, 0, angle.x / 3.14f * 180);
		// 移動
		transform.position += new Vector3 (Mathf.Sin (angle.x) * speed, Mathf.Cos (angle.x) * speed, 0);
		// 目的地まで着いたか
		if (Mathf.Abs (tagePos.x - transform.position.x) <= 0.5f &&
			Mathf.Abs (tagePos.y - transform.position.y) <= 0.5f) {
			// 向き
			transform.rotation = Quaternion.Euler (0, 0, 0);
			// 初期化
			status = Idol;
			attackStatus = None;
			arrival = false;
			stop = false;
			EfExplosion.SetActive (false);
		}
	}
		
	void SetAngle(){
		angle.x = Mathf.Atan2 (
			tagePos.x - transform.position.x,
			tagePos.y - transform.position.y
		);
		angle.y = Mathf.Atan2 (
			tagePos.y - transform.position.y,
			tagePos.x - transform.position.x
		);
	}
	void SetTargetAngle(){
		tagePos = armG.transform.position;
		angle.x = Mathf.Atan2 (
			tagePos.x - transform.position.x,
			tagePos.y - transform.position.y
		);
		angle.y = Mathf.Atan2 (
			tagePos.y - transform.position.y,
			tagePos.x - transform.position.x
		);
	}

	public void SetTage(Vector3 t){
		tagePos = t;
	}

	public byte GetStatus(){
		return status;
	}

	public void SetStatus(byte input){status = input;}
	public void SetAttackStatus(byte input){attackStatus = input;}

	void OnTriggerEnter2D(Collider2D other){
		if (status == Attack) {
			if (attackStatus == Normal) {
				GameObject obj = other.gameObject;
				if (obj.tag == "Enemy") {
					obj.GetComponent<CharacterStatus> ().ToIdol ();
					hitObj.Add (obj);
				} else if (obj.tag == "MotherShip") {
					obj.GetComponent<CharacterStatus> ().ReceiveDamage (10);
				}
			}
		}
	}

}
