using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    string scene_Login = "Login";
    string scene_Main = "Main";
    string scene_End = "End";

    public void ChangeScene(){
        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == scene_Login){
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene_Main);
        }
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == scene_Main){
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene_End);
        }
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == scene_End){
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene_Login);
        }
        
    }
}