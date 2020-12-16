using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] InputField PlayerNameInput = default;

    private void Start() {
        if(PlayerNameInput != null){
            PlayerNameInput.text = "Player " + Random.Range(1000, 10000);
        }
    }

    public void ChangeScene(){ 

        //スタート画面からメイン画面への遷移
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Login"){ 
            SceneManager.instance.OnLoginButtonClicked(PlayerNameInput.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main"); 
        }
        //メイン画面から終了画面への遷移
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main"){ 
            SceneManager.instance.OnEndButtonClicked();
            UnityEngine.SceneManagement.SceneManager.LoadScene("End"); 
        } 
        //終了画面からスタート画面への遷移
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "End"){ 
            UnityEngine.SceneManagement.SceneManager.LoadScene("Login"); 
        } 
    }
}