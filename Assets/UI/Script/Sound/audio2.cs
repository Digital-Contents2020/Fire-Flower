using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Sound_stopサウンドの再生と停止ボタンpart2

[RequireComponent(typeof(Button))]
public class audio2 : MonoBehaviour
{
    public GameObject b2;
    public AudioSource AudioSource;
    void Start() {
         //AudioSource.Stop(); //audioを止める
         
	}

	public void OnClick() { //ボタンクリックしたら
        AudioSource.Play(); //defaultで音を鳴らす
        Debug.Log ("停止ボタンclicked");
         b2.SetActive(false);
          b2.SetActive(true);
        this.gameObject.SetActive(false);//非アクティブ化
        //b1.SetActive(true);
    }
}
