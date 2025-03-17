using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private CharacterController character;
    public GameObject gameOverUI; // 게임 오버시 활성화할 UI
    public Text safeTimeText; // 점수를 표시할 UI
    public Text bestTime; // 게임 점수
    public Text phaseNum; // 게임 페이즈
    private float surviveTime; // 생존 시간
    public float phaseLv;
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        surviveTime = 0;
        gameOverUI.SetActive(false);
        character = FindAnyObjectByType<CharacterController>();

    }



    // Update is called once per frame
    void Update()
    {
        if (!character.isGameOver)
        {
            surviveTime += Time.deltaTime;
            safeTimeText.text = "Time: " + surviveTime.ToString("0.00");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }
        phaseLv = (surviveTime < 15f) ? 1 : 2;

        phaseNum.text = "Phase: " + phaseLv.ToString("0");
    }

    public void GameOver()
    {
        
        float bestScore = PlayerPrefs.GetFloat("bestTime");

        if (surviveTime > bestScore)
        {
            bestScore = surviveTime;
            PlayerPrefs.SetFloat("bestTime", bestScore);
            PlayerPrefs.Save(); // 저장된 값을 즉시 반영
        }

        bestTime.text = "Best Time: " + bestScore.ToString("0.00");
        gameOverUI.SetActive(true);

    }
}
