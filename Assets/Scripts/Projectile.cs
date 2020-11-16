using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 velocity;

    public int Id {get; private set;} // 弾のID
    public int OwnerId {get; private set;} // 弾を発射したプレイヤーのID
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;
    public bool IsActive => gameObject.activeSelf;

    public void Activate(int id, int ownerId, Vector3 origin, float angle) {
        Id = id;
        OwnerId = ownerId;
        transform.position = origin;
        velocity = 9f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

        gameObject.SetActive(true);
    }

    public void Deactivate(){
        gameObject.SetActive(false);
    }

    public void OnUpdate() {
        var dv = velocity * Time.deltaTime;
        transform.Translate(dv.x, dv.y, 0f);
    }

    // 画面外になった時に削除する（エディターのSceneビューの画面も影響するので注意）
    private void OnBecameInvisible() {
        Deactivate();
    }
}