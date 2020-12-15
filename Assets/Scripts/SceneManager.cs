using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourではなくMonoBehaviourPunCallbacksを継承して、Photonのコールバックを受け取れるようにする
public class SceneManager : MonoBehaviourPunCallbacks {

    private static SceneManager instance = null;

    [SerializeField] InputField PlayerNameInput = default;

    private void Awake() {
        if(instance == null)
         {
             instance = this;
             DontDestroyOnLoad(this.gameObject); 
         }
         else
         {
             Destroy(this.gameObject);
         }
    }

    private void Start () {
        // PhotonServerSettingsに設定した内容を使ってマスターサーバーへ接続する
        //PhotonNetwork.ConnectUsingSettings ();
        //PhotonNetwork.LocalPlayer.NickName = "Player";

        PlayerNameInput.text = "Player " + Random.Range(1000, 10000);
    }


    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster () {
        string displayName = $"{PhotonNetwork.NickName}の部屋";
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom ("room", GameRoomProperty.CreateRoomOptions (displayName), TypedLobby.Default);
    }

    // マッチングが成功した時に呼ばれるコールバック
    public override void OnJoinedRoom () {
        // マッチング後、ランダムな位置に自分自身のネットワークオブジェクトを生成する
        var v = new Vector3 (Random.Range (-3f, 3f), Random.Range (-3f, 3f));
        var r = new Quaternion(90,0,0,0);
        PhotonNetwork.Instantiate ("FireFlower", v, Quaternion.Euler(-90f,0f,0f));

        // 現在のサーバー時刻を、ゲームの開始時刻に設定する
        if (PhotonNetwork.IsMasterClient && !PhotonNetwork.CurrentRoom.HasStartTime ()) {
            PhotonNetwork.CurrentRoom.SetStartTime (PhotonNetwork.ServerTimestamp);
        }
    }

    public void OnLoginButtonClicked()
        {
            string playerName = PlayerNameInput.text;

            if (!playerName.Equals(""))
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();

                LoadScene.ChangeScene();
            }
            else
            {
                Debug.LogError("Player Name is invalid.");
            }
        }
}