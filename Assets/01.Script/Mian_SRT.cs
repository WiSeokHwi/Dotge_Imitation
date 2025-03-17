using UnityEngine;
using UnityEngine.SceneManagement;


public class Mian_SRT : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void Exit()
    {
        Application.Quit(); 
    }
}
