using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchKeyBoard : MonoBehaviour
{
    public string stringToEdit = "Hello World!";

    [SerializeField] Text text;

    private TouchScreenKeyboard keyboard;

    private void Update() {
        if(keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done){

        }
    }

    public void OnButtonClicked(){
        keyboard = TouchScreenKeyboard.Open("Enter Your Name");

        keyboard.text = "なぎさああああああ";
    }
}
