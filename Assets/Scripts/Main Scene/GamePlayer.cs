﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    GameObject particle;

    [SerializeField]
    private Text nameLabel = default;
    private Rigidbody rb = null;

    public Player Owner => photonView.Owner;

    Vector3 defaultPos;
    Vector3 prevPos;

    //パーティクルたち
    MeshRenderer mr;
    float fadeSpeed = 0.02f;
    float alfa;


    //フラグ
    bool isActive = false;
    bool isFadeIn = false;
    bool isFadeOut = false;
    bool isRespawn = false;


    private void Awake() {

        var gamePlayerManager = GameObject.FindWithTag("GamePlayerManager").GetComponent<GamePlayerManager>();
        transform.SetParent(gamePlayerManager.transform);
    }

    void Start()
    {
        nameLabel.text = photonView.Owner.NickName;

        rb = GetComponent<Rigidbody>();
        isActive = true;
        
        mr = GetComponent<MeshRenderer>();
        alfa = mr.material.color.a;
        particle.SetActive(false);
        
    }

    private void Update() {
        if (photonView.IsMine && isActive) {
            var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0).normalized;
            // Debug.Log(direction);
            var dv = 6f * direction;
            rb.velocity = new Vector3(dv.x, dv.y, 0f);

            if(Input.GetKeyDown("t")){
                Debug.Log("press t");
                photonView.RPC(nameof(Ignition), RpcTarget.All);
            }
            if(Input.GetKeyDown("f")){
                Debug.Log("press f");
                isFadeOut = true;
            }
        }

        if(isFadeOut) FadeOut();
        if(isFadeIn) FadeIn();
        if(isRespawn) Respawn();
    }

    // データを送受信するメソッド
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // 自身側が生成したオブジェクトの場合は
            // 色相値と移動中フラグのデータを送信する
            stream.SendNext(isFadeOut);
            stream.SendNext(isFadeIn);
            stream.SendNext(isRespawn);
        } else {
            // 他プレイヤー側が生成したオブジェクトの場合は
            // 受信したデータから色相値と移動中フラグを更新する
            isFadeOut = (bool)stream.ReceiveNext();
            isFadeIn = (bool)stream.ReceiveNext();
            isRespawn = (bool)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (photonView.IsMine) {
            if(other.tag == "Fire"){
                photonView.RPC(nameof(Ignition), RpcTarget.All);
            }
            else if(other.tag == "Water"){
                photonView.RPC(nameof(BurnOut), RpcTarget.All);
            }
        }
    } 


    //火が付いたとき
    [PunRPC]
    void Ignition(){
        particle.SetActive(true);
    }

    // 火が消えたとき
    [PunRPC]
    void BurnOut(){
        particle.SetActive(false);
        isActive = false;
        if(!isRespawn) isFadeOut=true;
    }

    //リスポーン処理
    void Respawn(){
        transform.position = defaultPos;
        this.gameObject.SetActive(true);
        isFadeIn = true;
        isRespawn = false;
    }

    //フェードアウト
    public void FadeOut(){
        alfa = alfa - fadeSpeed;
        mr.material.color = mr.material.color - new Color(0,0,0,fadeSpeed);
        if(alfa <= 0f){
            isFadeOut = false;
            isRespawn = true;
        }
    }

    //フェードイン
    public void FadeIn(){
        alfa = alfa + fadeSpeed;
        mr.material.color = mr.material.color + new Color(0,0,0,fadeSpeed);
        if(alfa >= 1f){
            isFadeIn = false;
            isActive = true;
        }
    }
}