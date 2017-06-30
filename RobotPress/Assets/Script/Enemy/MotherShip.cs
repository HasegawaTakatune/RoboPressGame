using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : CharacterStatus {
	// 一度のみの実行
	private bool DoOnce = false;
	// 移動量
	private Vector3 amountOfMovement;
	// 前進時間
	private float advanceTime;
	// 子のスポナーを格納
	private Spawner spawner;

	// Use this for initialization
	void Start () {
		// Super
		status = Active;
		speed = 0.03f;
		vitality = 30;
		score = 50;
		// スポナー取得
		spawner = GetComponentInChildren<Spawner> ();
		// 移動量初期化
		amountOfMovement = new Vector3 (
			Mathf.Sin ((transform.localEulerAngles.y + 270) * 3.14f / 180) * speed,
			Mathf.Cos ((transform.localEulerAngles.y + 270) * 3.14f / 180) * speed,
			0
		);
		// 移動し続ける時間
		advanceTime = Random.Range(4,6);
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}
	// 停止処理
	protected override void IdolAction ()
	{
		// 敵機の排出
		if (!DoOnce) {
			spawner.OnActivate ();
			DoOnce = true;
		}
	}
	// 行動処理
	protected override void ActiveAction ()
	{
		// 移動
		transform.position += amountOfMovement;
		if (!DoOnce) {
			// 浮遊行動にシフト
			StartCoroutine (myFunc.Delay (advanceTime, () => {
				status = Idol;
				DoOnce = false;
			}));
			DoOnce = true;
		}
	}
	// 死亡処理
	protected override void DeadAction ()
	{
		GameStatus.motherShipDestroyed++;
		base.DeadAction ();
	}
}
