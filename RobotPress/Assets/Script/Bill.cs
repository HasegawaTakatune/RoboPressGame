using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bill : CharacterStatus {
	// 移動量
	private Vector3 amountOfMovement;

	// Use this for initialization
	void Start () {
		status = Idol;
	}

	protected override void Update ()
	{
		base.Update ();
	}
	// 停止処理
	protected override void IdolAction ()
	{
	}
	// 行動処理
	protected override void ActiveAction ()
	{
	}
	// 死亡処理
	protected override void DeadAction ()
	{
	}
}
