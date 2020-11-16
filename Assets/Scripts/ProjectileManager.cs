using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    private Projectile projectilePrefab = default; // Projectileのprefabの参照

    // アクティブな弾のリスト
    private List<Projectile> activeList = new List<Projectile>();
    // 非アクティブな弾のオブジェクトプール
    private Stack<Projectile> inactivePool = new Stack<Projectile>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 逆順にループを回して、activeListの要素が途中で削除されても正しくループが回るようにする
        for(int i=activeList.Count - 1;i>=0;i--){
            var projectile = activeList[i];
            if(projectile.IsActive){
                projectile.OnUpdate();
            } else {
                Remove(projectile);
            }
        }
    }

    // 弾を発射（アクティブ化）するメソッド
    public void Fire(Vector3 origin, float angle){
        // 非アクティブの弾があれば使い回す、なければ生成する
        var projectile = (inactivePool.Count > 0)
            ? inactivePool.Pop()
            : Instantiate(projectilePrefab, transform);
        projectile.Activate(origin, angle);
        activeList.Add(projectile);
    }

    // 弾を消去（非アクティブ化）するメソッド
    public void Remove(Projectile projectile){
        activeList.Remove(projectile);
        projectile.Deactivate();
        inactivePool.Push(projectile);
    }
}
