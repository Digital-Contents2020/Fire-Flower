using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

// MonoBehaviourPunCallbacksを継承すると、photonViewプロパティが使えるようになる
public class GamePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    private ProjectileManager projectileManager;

    private SpriteRenderer spriteRenderer;
    private float hue = 0f;
    private bool isMoving = false;

    private Camera cam;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeBodyColor();
        
        projectileManager = GameObject.FindWithTag("ProjectileManager").GetComponent<ProjectileManager>();
    }

    private void Update() {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine) {
            // 入力方向を正規化
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            // 移動速度を時間に依存させて、移動量を求める
            var dv = 6f * Time.deltaTime * direction;
            transform.Translate(dv.x, dv.y, 0f);
            
            // 移動中なら色相変化
            isMoving = direction.magnitude > 0f;
            if(isMoving){
                hue = (hue + Time.deltaTime) % 1f;
            }

            ChangeBodyColor();

            // 左クリックでカーソルの方向に弾を発射する処理を行う
            if (Input.GetMouseButtonDown(0)) {
                var playerWorldPosition = transform.position;
                var mousePos = Input.mousePosition;
                mousePos.z = 10.0f;
                var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
                var dp = mouseWorldPosition - playerWorldPosition;
                float angle = Mathf.Atan2(dp.y, dp.x);

                photonView.RPC(nameof(FireProjectile), RpcTarget.All, angle); 
            }
        }
    }

    // 弾を発射するメソッド
    [PunRPC]
    private void FireProjectile(float angle) {
       projectileManager.Fire(transform.position, angle);
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
