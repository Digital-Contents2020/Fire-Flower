using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class GamePlayer : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshPro nameLabel = default;
    private Rigidbody rb = null;

    private BulletManager bulletManager;
    private int bulletId = 0;

    public Player Owner => photonView.Owner;

    private void Awake() {
        bulletManager = GameObject.FindWithTag("BulletManager").GetComponent<BulletManager>();
        rb = GetComponent<Rigidbody>();

        var gamePlayerManager = GameObject.FindWithTag("GamePlayerManager").GetComponent<GamePlayerManager>();
        transform.SetParent(gamePlayerManager.transform);
    }

    private void Update() {
        if (photonView.IsMine) {
            var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0).normalized;
            Debug.Log(direction);
            var dv = 6f * direction;
            rb.velocity = new Vector3(dv.x, dv.y, 0f);

            // 左クリックでカーソルの方向に弾を発射する処理を行う
            if (Input.GetMouseButtonDown(0)) {
                var playerWorldPosition = transform.position;
                var mousePos = Input.mousePosition;
                mousePos.z = 10.0f;
                var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
                var dp = mouseWorldPosition - playerWorldPosition;
                float angle = Mathf.Atan2(dp.y, dp.x);

                // FireBullet(angle)をRPCで実行する
                photonView.RPC(nameof(FireBullet), RpcTarget.All, transform.position, angle);
            }
        }
    }
    
    // 弾を発射するメソッド
    [PunRPC]
    private void FireBullet(Vector3 origin, float angle, PhotonMessageInfo info)  {
        int timestamp = info.SentServerTimestamp;
        bulletManager.Fire(timestamp, photonView.OwnerActorNr, origin, angle, timestamp);
    }

    private void OnTriggerEnter(Collider collision) {
        if (photonView.IsMine) {
            var bullet = collision.GetComponent<Bullet>();
            if (bullet != null && bullet.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber) {
                photonView.RPC(nameof(HitByBullet), RpcTarget.All, bullet.Id, bullet.OwnerId);
            }
        }
    }

    [PunRPC]
    private void HitByBullet(int bulletId, int ownerId) {
        bulletManager.Remove(bulletId, ownerId);
    }  
}