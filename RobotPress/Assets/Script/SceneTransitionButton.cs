using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionButton : MonoBehaviour {
	// シーンの移動先を選択
	[SerializeField] private string loadSceneName;
	// ボタンコンポーネント
	private Button button;
	// オーディオクリップ
	private AudioClip audioClip;
	// オーディオソース
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		// ボタンコンポーネントの格納
		button = GetComponent<Button> ();
		// オーディオソースの格納
		audioSource = GetComponent<AudioSource>();
		// オーディオソースのクリップ設定
		audioSource.clip = audioClip;
		// ボタンが押された時の処理を追加
		button.onClick.AddListener (OnClickSceneButton);
	}
		
	//
	private void OnPlayOnshot(){
		
	}
	// シーン移動処理
	private void OnClickSceneButton(){
		SceneManager.LoadScene (loadSceneName);
	}
}
