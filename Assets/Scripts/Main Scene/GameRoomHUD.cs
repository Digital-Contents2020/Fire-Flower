using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameRoomHUD : MonoBehaviour
{
    private Text timeLabel;

    private void Awake() {
        timeLabel = GetComponent<Text>();
    }

    private void Update() {
        // まだルームに参加していない時は更新しない
        if (!PhotonNetwork.InRoom) { return; }
        // まだゲーム開始時刻が設定されていない時は更新しない
        if (!PhotonNetwork.CurrentRoom.TryGetStartTime(out int timestamp)) { return; }

        // ゲーム開始時刻からの経過時間を求めて、テキスト表示する
        float elapsedTime = Mathf.Max(unchecked(PhotonNetwork.ServerTimestamp - timestamp) / 1000f);
        timeLabel.text = elapsedTime.ToString("f2"); // 小数点以下2桁表示
    }
}