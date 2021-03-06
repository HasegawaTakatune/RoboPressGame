﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterStatus {
	// ステータス値
	private const byte
	Move = 0,
	Spawn = 1;
	private byte activAction;
	// 移動角度
	[SerializeField]private float angle;
	// 遅延時間
	private float delayTime;

	private bool isTargetting = false;

	// Use this for initialization
	void Start () {
		// Super
		status = Active;
		speed = 0.05f;
		vitality = 10;
		score = 10;

		activAction = Spawn;
		angle = Random.Range (225, 315);
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}
	// 停止処理
	protected override void IdolAction(){
		
	}
	// 行動処理
	protected override void ActiveAction ()
	{
		// 行動パターンの選択
		switch (activAction) {
		case Move:
			MoveAction ();
			break;
		case Spawn:
			SpawnAction ();
			break;
		}
	}
	// 死亡処理
	protected override void DeadAction ()
	{
		GameStatus.flyEnemyDestroyed++;
		base.DeadAction ();
	}
	// 通常行動
	private void MoveAction(){
		// 移動
		if (isTargetting) {
			
		} else {
			transform.position += new Vector3 (
				Mathf.Sin ((transform.localEulerAngles.y + angle) * 3.14f / 180) * base.speed,
				Mathf.Cos ((transform.localEulerAngles.y + angle) * 3.14f / 180) * base.speed,
				0
			);
			if (transform.position.x <= -10)
				Destroy (this.gameObject);
		}
	}
	// スポーン時の行動
	private void SpawnAction(){
		transform.position += new Vector3 (
			0,//Mathf.Sin ((transform.localEulerAngles.y + angle) * 3.14f / 180) * base.speed,
			Mathf.Cos ((transform.localEulerAngles.y + angle) * 3.14f / 180) * base.speed,
			0
		);
		delayTime = Random.Range (0.5f, 6);
		StartCoroutine (myFunc.Delay (delayTime, () => {
			activAction = Move;
			angle = 270;
		}));
	}
}
