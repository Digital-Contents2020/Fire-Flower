using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

// MonoBehaviourPunCallbacksを継承すると、photonViewプロパティが使えるようになる
public class GamePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    private SpriteRenderer spriteRenderer;
    private float hue = 0f;
    private bool isMoving = false;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeBodyColor();
    }

    private void Update() {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine) {
            // 入力方向を正規化
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            // 移動速度を時間に依存させて、移動量を求める
            var dv = 6f * Time.deltaTime * direction;
            transform.Translate(dv.x, dv.y, 0f);

            isMoving = direction.magnitude > 0f;
            if(isMoving){
                hue = (hue + Time.deltaTime) % 1f;
            }

            ChangeBodyColor();
        }
    }

    // データを送受信するメソッド
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting) {
            // 自身側が生成したオブジェクトの場合は色相値と移動中フラグのデータを送信する
            stream.SendNext(hue);
            stream.SendNext(isMoving);
        } else {
            // 他プレイヤー側が生成したオブジェクトの場合は受信したデータから色相値と移動中フラグのデータを更新する
            hue = (float)stream.ReceiveNext();
            isMoving = (bool)stream.ReceiveNext();

            ChangeBodyColor();
        }
    }

    private void ChangeBodyColor(){
        float h = hue;
        float s = 1f;
        float v = (isMoving) ? 1f : 0.5f;
        spriteRenderer.color = Color.HSVToRGB(h, s, v);
    }
}
