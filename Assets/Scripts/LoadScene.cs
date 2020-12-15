using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    string scene_Login = "Login"; 
    string scene_Main = "Main"; 
    string scene_End = "End"; 
 
    public static void ChangeScene(){ 
        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); 
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Login"){ 
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main"); 
        } 
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main"){ 
            UnityEngine.SceneManagement.SceneManager.LoadScene("End"); 
        } 
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "End"){ 
            UnityEngine.SceneManagement.SceneManager.LoadScene("Login"); 
        } 
    }
}