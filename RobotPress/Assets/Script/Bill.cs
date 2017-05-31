using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bill : CharacterStatus {
	// 移動量
	private Vector3 amountOfMovement;

	// Use this for initialization
	void Start () {
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
		transform.position += new Vector3 (
			Mathf.Sin ((transform.localEulerAngles.y + 270) * 3.14f / 180) * 0.01f,
			0, 0);
	}
	// 死亡処理
	protected override void DeadAction ()
	{
		base.DeadAction ();
	}
}
