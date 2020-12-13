using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Fade : MonoBehaviour
{
    [SerializeField]
    MeshRenderer mr;
    float fadeSpeed = 0.002f;
    float alfa;
    bool isFadeIn = false;
    bool isFadeOut = false;
    bool isRespawn = false;
    // Start is called before the first frame update
    void Start()
    {
        // mr = GetComponent<MeshRenderer>();
        alfa = mr.material.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f")){
            if(!isRespawn){
                isFadeOut=true;
            }
            else{
                isFadeIn=true;
            }
        }

        if(isFadeOut){
            FadeOut();
        }
        if(isFadeIn){
            FadeIn();
        }
    }

    public void FadeOut(){
        alfa = alfa - fadeSpeed;
        mr.material.color = mr.material.color - new Color(0,0,0,fadeSpeed);
        if(alfa <= 0f){
            isFadeOut = false;
            isRespawn = true;
        }
    }

    public void FadeIn(){
        alfa = alfa + fadeSpeed;
        mr.material.color = mr.material.color + new Color(0,0,0,fadeSpeed);
        if(alfa >= 1f){
            isFadeIn = false;
            isRespawn = false;
        }
    }
}
