using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Sound_stopサウンドの再生と停止ボタンpart1

[RequireComponent(typeof(Button))]
public class audio : MonoBehaviour
{
    
    public Sprite spritePlay;
    public Sprite spriteStop;

    Image image;
    AudioSource BGM;
    void Start() {
	}
                    // b1.SetActive(false);
                    //b1=GetComponent<Button>();//buttonコンポネ取得
        image = GetComponent<Image>();
        BGM = GetComponent<AudioSource>();
    }

	public void OnClick() { //ボタンクリックしたら

        if(BGM.isPlaying){
            BGM.Stop();
            image.sprite = spriteStop;
        }
        else{
            BGM.Play();
            image.sprite = spritePlay;
        }
        // BGM.Stop(); //audioを止める
        Debug.Log ("clicked");
        // this.gameObject.SetActive(false);//非アクティブ化
        //b1.SetActive(true);
        // b1.SetActive(true);
    }
}
