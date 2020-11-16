using Photon.Pun;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承すると、photonViewプロパティが使えるようになる
public class GamePlayer : MonoBehaviourPunCallbacks
{
    private void Update() {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine) {
            // 入力方向を正規化
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            // 移動速度を時間に依存させて、移動量を求める
            var dv = 6f * Time.deltaTime * direction;
            transform.Translate(dv.x, dv.y, 0f);
        }
    }
}
