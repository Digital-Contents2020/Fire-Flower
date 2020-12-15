using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public static class GameRoomProperty
{
    private const string KeyDisplayName = "DisplayName"; // 表示用ルーム名のキーの文字列
    private const string KeyStartTime = "StartTime"; // ゲーム開始時刻のキーの文字列

    private static Hashtable hashtable = new Hashtable();

    // ルームの初期設定オブジェクトを作成する
    public static RoomOptions CreateRoomOptions(string displayName) {
        return new RoomOptions() {
            // カスタムプロパティの初期設定
            CustomRoomProperties = new Hashtable() {
                { KeyDisplayName, displayName }
            },
            // ロビーからカスタムプロパティを取得できるようにする
            CustomRoomPropertiesForLobby = new string[] {
                KeyDisplayName
            }
        };
    }

    // 表示用ルーム名を取得する
    public static string GetDisplayName(this Room room) {
        return (string)room.CustomProperties[KeyDisplayName];
    }

    // ゲーム開始時刻が設定されているか調べる
    public static bool HasStartTime(this Room room) {
        return room.CustomProperties.ContainsKey(KeyStartTime);
    }

    // ゲーム開始時刻があれば取得する
    public static bool TryGetStartTime(this Room room, out int timestamp) {
        if (room.CustomProperties[KeyStartTime] is int value) {
            timestamp = value;
            return true;
        }
        timestamp = 0;
        return false;
    }

    // ゲーム開始時刻を設定する
    public static void SetStartTime(this Room room, int timestamp) {
        hashtable[KeyStartTime] = timestamp;

        room.SetCustomProperties(hashtable);
        hashtable.Clear();
    }
}