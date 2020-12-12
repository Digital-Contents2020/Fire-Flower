using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefab = default; // BulletのPrefabの参照

    // アクティブな弾のリスト
    private List<Bullet> activeList = new List<Bullet>();
    // 非アクティブな弾のオブジェクトプール
    private Stack<Bullet> inactivePool = new Stack<Bullet>();

    private void Update() {
        // 逆順にループを回して、activeListの要素が途中で削除されても正しくループが回るようにする
        for (int i = activeList.Count - 1; i >= 0; i--) {
            var bullet = activeList[i];
            if (bullet.IsActive) {
                bullet.OnUpdate();
            } else {
                Remove(bullet);
            }
        }
    }

    // 弾を発射（アクティブ化）するメソッド
    public void Fire(int id, int ownerId,Vector3 origin, float angle, int timestamp) {
        // 非アクティブの弾があれば使い回す、なければ生成する
        var bullet = (inactivePool.Count > 0)
            ? inactivePool.Pop()
            : Instantiate(bulletPrefab, transform);
        bullet.Activate(id, ownerId, origin, angle, timestamp);
        activeList.Add(bullet);
    }

    // 弾を消去（非アクティブ化）するメソッド
    public void Remove(Bullet bullet) {
        activeList.Remove(bullet);
        bullet.Deactivate();
        inactivePool.Push(bullet);
    }

    public void Remove(int id, int ownerId) {
        foreach (var bullet in activeList) {
            if (bullet.Equals(id, ownerId)) {
                Remove(bullet);
                break;
            }
        }
    }
}