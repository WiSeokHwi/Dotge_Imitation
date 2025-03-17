using UnityEngine;
using UnityEngine.SceneManagement;


public class Mian_SRT : MonoBehaviour
{
    public void StartMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void Exit()
    {
        Application.Quit(); 
    }
}
