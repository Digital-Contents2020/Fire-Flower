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
        image = GetComponent<Image>();
        BGM = GetComponent<AudioSource>();
        BGM.Play();
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
        Debug.Log ("clicked");
    }
}
