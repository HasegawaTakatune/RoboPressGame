using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : CharacterStatus {

	[SerializeField]private GameObject spawnObj;
	private float delayTime = 2.5f;
	private bool usedSpawn = false;
	private bool DoOnce = false;

	// Use this for initialization
	void Start () {
		status = Idol;
		speed = 0.1f;
		vitality = 300;
		score = 500;
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}

	protected override void ActiveAction ()
	{
		// 移動
		transform.position += new Vector3 (
			Mathf.Sin ((transform.localEulerAngles.y + 270) * 3.14f / 180) * base.speed,
			Mathf.Cos ((transform.localEulerAngles.y + 270) * 3.14f / 180) * base.speed,
			0
		);
	}
	protected override void IdolAction ()
	{
		if (usedSpawn) {
			delayTime = Random.Range (3, 5);
			usedSpawn = false;
			// 生成処理を要請
			StartCoroutine (myFunc.Delay (delayTime, () => {
				Spawn();
				usedSpawn = true;
			}));
		}

		if (!DoOnce && GameStatus.GetScore () >= 500) {
			DoOnce = true;
			usedSpawn = true;
			status = Active;
			StartCoroutine (myFunc.Delay (delayTime, () => {
				status = Idol;
			}));
		}
	}
	// 生成
	private void Spawn(){
		int spawnNum = Random.Range (10, 15), i = 0;
		while (i < spawnNum) {
			Instantiate (spawnObj, transform.position, Quaternion.identity);
			i++;
		}
	}
}
