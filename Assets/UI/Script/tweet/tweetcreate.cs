using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ツイート機能のサンプル
/// </summary>
public class tweetcreate : MonoBehaviour
{
    // 各種パラメーターはインスペクターから設定する
    [SerializeField] Button tweetButton;                        // ツイートするボタン
    [SerializeField] string text = "ツイート機能のテスト中";    // ツイートに挿入するテキスト
    [SerializeField] string linkUrl = "http://negi-lab.blog.jp/";   // ツイートに挿入するURL
    [SerializeField] string hashtags = "Unity,ねぎらぼ";        // ツイートに挿入するハッシュタグ

    // ツイート画面を開く
    private void Tweeting ()
    {
        var url = "https://twitter.com/intent/tweet?"
            + "text=" + text
            + "&url=" + linkUrl
            + "&hashtags=" + hashtags;
            Debug.Log("createurl");

        #if UNITY_EDITOR
            Application.OpenURL ( url );
        #elif UNITY_WEBGL
            // WebGLの場合は、ゲームプレイ画面と同じウィンドウでツイート画面が開かないよう、処理を変える
            Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
        #else
            Application.OpenURL(url);
        #endif
    }

    // UIボタンのクリックでツイート画面を開く場合
    private void Start ()
    {
        tweetButton.onClick.AddListener ( () =>
        {
            Tweeting ();
            Debug.Log("onclick");
        } );
    }

    // マウスの右クリックでツイート画面を開く場合
    private void Update ()
    {
        if ( Input.GetMouseButtonDown ( 1 ) )
        {
            Tweeting ();
            Debug.Log("right-onclick");
        }
    }
}
