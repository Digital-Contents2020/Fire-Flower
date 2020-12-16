using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireworks : MonoBehaviour
{
    [SerializeField]
    private GameObject explodeAudioObj;
    private ParticleSystem seedPs;
    // Start is called before the first frame update
    void Start()
    {
        seedPs = GetComponent<ParticleSystem>();
        StartCoroutine(ProgressCo());
        Debug.Log("seed取得");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private int getSubEmitterParticleNum()
    {
        int ptNum = 0;
        ParticleSystem[] psArr = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in psArr)
        {
            ptNum += ps.particleCount;
            Debug.Log(ptNum);
        }
        return ptNum;
    }

    IEnumerator ProgressCo()
    {
        // ひゅるるる待ち
        while (seedPs.particleCount == 0)
        {
            yield return null;
            Debug.Log("ヒュルル町");
        }
        // ひゅるるるの間は音の位置を移動
        while (seedPs.particleCount > 0)
        {
           /* ParticleSystem.Particle[] pps = new ParticleSystem.Particle[seedPs.particleCount];
            explodeAudioObj.transform.localPosition = pps[0].position;
            yield return null;
            Debug.Log("音の位置いどう");*/
        }
        // 爆発待ち
        while (getSubEmitterParticleNum() == 0)
        {
            yield return null;
            Debug.Log("爆発町");
        }
        // 爆発音
        explodeAudioObj.GetComponent<AudioSource>().pitch *= Random.Range(0.8f, 1.2f);
        Debug.Log("爆発音");
        explodeAudioObj.GetComponent<AudioSource>().Play();
        // 消滅待ち
        while (getSubEmitterParticleNum() > 0)
        {
            yield return null;
            Debug.Log("消滅町");
        }
        // 消滅
        Destroy(gameObject);
    }

}
