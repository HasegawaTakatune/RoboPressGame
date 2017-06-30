using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	private Camera cam;
	// 画像のサイズ
	private float width = 1600f;
	private float height = 1000f;
	// 画像のPixel Per Unit
	private float pixelPerUnit = 100f;

	void Awake () {
		// カメラコンポーネントを取得します
		cam = GetComponent<Camera> ();
		// カメラのorthographicSizeを設定
		cam.orthographicSize = height / 2f / pixelPerUnit;
	}
}
