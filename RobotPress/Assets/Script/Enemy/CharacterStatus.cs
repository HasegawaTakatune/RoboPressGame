using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour {
	[SerializeField]GameObject EfExplosion;
	// ステータス
	protected const byte Idol = 0,Active = 1,Dead = 2,Shot = 3;
	protected byte status = Active;
	// 速度
	protected float speed;
	// 射出速度
	private float shotSpeed = 0.5f;
	// 体力(活力)
	protected int vitality;
	// ダメージ
	protected int damage = 10;
	// 
	protected int score;

	protected virtual void Update(){
		switch (status) {
		case Idol:
			IdolAction ();
			break;
		case Active:
			ActiveAction ();
			break;
		case Shot:
			ShotAction ();
			break;
		case Dead:
			DeadAction ();
			break;
		default:
			break;
		}
	}
	// 停止時処理
	protected virtual void IdolAction(){
	}
	// 行動時処理
	protected virtual void ActiveAction(){
	}
	// 死亡時処理
	protected virtual void DeadAction(){
		Destroy (this.gameObject);
	}
	protected virtual void DeadAction(float waitTime){
		Destroy (this.gameObject, waitTime);
	}

	// ダメージを受ける
	public void ReceiveDamage(int dmg){
		vitality -= dmg;
		Instantiate (EfExplosion, transform.position, transform.rotation);
		// 体力がなくなったら
		if (vitality <= 0) {
			GameStatus.AddScore (score);
			//GameStatus.AddNumberDestroyed ();
			GameUI.SetScore ();
			status = Dead;
		}
	}
	// ステータスのセット
	public void ToIdol(){status = Idol;}
	// ショット！！
	public void ActivateShot(){ status = Shot; Destroy(gameObject,5.0f);}
	protected void ShotAction(){
		transform.position += new Vector3 (
			Mathf.Sin ((transform.localEulerAngles.y + 90) * 3.14f / 180) * shotSpeed,
			Mathf.Cos ((transform.localEulerAngles.y + 90) * 3.14f / 180) * shotSpeed,
			0
		);
	}
	// 当たり判定
	private void OnTriggerEnter2D(Collider2D other){
		if (status == Shot && other.gameObject.tag == "Enemy" || other.gameObject.tag == "MotherShip") {
			other.gameObject.GetComponent<CharacterStatus> ().ReceiveDamage (damage);
			Instantiate (EfExplosion, transform.position, transform.rotation);
		}
	}
}