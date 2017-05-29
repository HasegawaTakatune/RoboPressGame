using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCatchArms : MonoBehaviour {
	// もう一方の腕
	[SerializeField]
	private GameObject otherArms;
	// 親オブジェクト
	[SerializeField]
	private GameObject parentObj;
	// 角度
	private Vector2 angle;
	// 速度
	private float speed = 0.1f;
	// 移動制御
	public bool movement = true;
	// Use this for initialization
	void Start () {
		angle.x = Mathf.Atan2 (
			otherArms.transform.position.x - transform.position.x,
			otherArms.transform.position.y - transform.position.y
		);
		angle.y = Mathf.Atan2 (
			otherArms.transform.position.y - transform.position.y,
			otherArms.transform.position.x - transform.position.x
		);
	}
	
	// Update is called once per frame
	void Update () {
		// 移動する
		MoveAction ();
	}
	// 移動処理
	private void MoveAction(){
		if (movement) {
			transform.position += new Vector3 (Mathf.Sin (angle.x) * speed, Mathf.Cos (angle.x) * speed, 0);
		}
	}
	// 当たり判定
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Bill") {
			movement = false;
			// 当たった対象を子関係にする
			parentObj.GetComponent<CatchArms> ().ParentSetting (other.gameObject);
		}
	}
	// 削除要請
	public void CallDestroy(){
		Destroy (this.gameObject);
	}
}
