using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//パネルの中にbuttonが入ってるはずなのに取れないのなんで？
public class sousa2 : MonoBehaviour
{
    public GameObject panel;
    public GameObject button;
    public GameObject loginpanel;
    void Start()
    {
        //panel.SetActive(false);
    }

     public void OnClick() { //ボタンクリックしたら
        Debug.Log ("Return clicked2");
        panel.SetActive(true);
        button.SetActive(true);
        loginpanel.SetActive(false);
    }
   
}
