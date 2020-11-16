using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 velocity;

    public void Init(Vector3 origin, float angle) {
        transform.position = origin;
        velocity = 9f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void Update() {
        var dv = velocity * Time.deltaTime;
        transform.Translate(dv.x, dv.y, 0f);
    }

    // 画面外になった時に削除する（エディターのSceneビューの画面も影響するので注意）
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}