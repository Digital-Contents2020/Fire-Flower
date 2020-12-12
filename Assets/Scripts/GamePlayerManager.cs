using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerManager : MonoBehaviour
{
    private List<GamePlayer> playerList = new List<GamePlayer>();

    public GamePlayer this[int index] => playerList[index];
    public int count => playerList.Count;

    private void OnTransformChildrenChanged(){
        //子要素が変わったら、ネットワークオブジェクトのリストを更新
        playerList.Clear();
        foreach(Transform child in transform){
            playerList.Add(child.GetComponent<GamePlayer>());
        }
    }
}
