using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//パネルの中にbuttonが入ってるはずなのに取れないのなんで？
public class sousa2 : MonoBehaviour
{
    public GameObject panel;
    public GameObject button;
    public GameObject loginpanel;
    public GameObject items;

    void Start()
    {
        //panel.SetActive(false);
    }

     public void OnClick() { //ボタンクリックしたら
        Debug.Log ("Return clicked2");
        if(this.gameObject.tag == "return"){

            panel.SetActive(false);
            button.SetActive(false);
            loginpanel.SetActive(true);
            items.SetActive(true);
        }
        else if(this.tag == "explain"){
            panel.SetActive(true);
            button.SetActive(true);
            loginpanel.SetActive(false);
            items.SetActive(false);
        }
    }
   
}
