using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Anim : MonoBehaviour
{
    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
       particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(particle.isStopped){
            Debug.Log("stop");
        }
    }
}
