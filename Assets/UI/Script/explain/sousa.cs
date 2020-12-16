using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sousa : MonoBehaviour
{
    public GameObject panel;
    void Start(){
       // Debug.Log("何からし");
       // title.SetActive(false);
        //panel.SetActive(false);
    }
    public void OnClick() { //ボタンクリックしたら
        Debug.Log ("Return clicked");
        //this.gameObject.SetActive(false);//非アクティブ化
        panel.SetActive(false);
    }
}
