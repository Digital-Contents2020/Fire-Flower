using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Sound_stopサウンドの再生と停止ボタンpart1

[RequireComponent(typeof(Button))]
public class audio : MonoBehaviour
{
    public GameObject b1;
    public AudioSource AudioSource;
    void Start() {
        AudioSource.Play(); //defaultで音を鳴らす
         b1.SetActive(false);
        //b1=GetComponent<Button>();//buttonコンポネ取得
       
	}

	public void OnClick() { //ボタンクリックしたら
        AudioSource.Stop(); //audioを止める
        Debug.Log ("clicked");
        this.gameObject.SetActive(false);//非アクティブ化
        //b1.SetActive(true);
        b1.SetActive(true);
    }
}
