using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Anim : MonoBehaviour
{
    Animator animator;
    int trans;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        trans = animator.GetInteger("trans");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            trans++;
            Debug.Log(trans);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            trans--;
            
            Debug.Log(trans);
        }

        animator.SetInteger("trans", trans);
    }
}
