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
        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); 

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Login"){ 
            Debug.Log(PlayerNameInput.text);

            SceneManager.instance.OnLoginButtonClicked(PlayerNameInput.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main"); 
            
        } 
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main"){ 
            SceneManager.instance.OnEndButtonClicked();
            UnityEngine.SceneManagement.SceneManager.LoadScene("End"); 
        } 
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "End"){ 
            UnityEngine.SceneManagement.SceneManager.LoadScene("Login"); 
        } 
    }
}