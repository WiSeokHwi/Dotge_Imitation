using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private CharacterController character;
    public GameObject gameOverUI; // ���� ������ Ȱ��ȭ�� UI
    public Text safeTimeText; // ������ ǥ���� UI
    public Text bestTime; // ���� ����
    public Text phaseNum; // ���� ������
    private float surviveTime; // ���� �ð�


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
                SceneManager.LoadScene("SampleScene");
            }

        }
    }
    public void phaseLevel()
    {
        int phase = PlayerPrefs.GetInt("phase");
        phaseNum.text = "Phase: " + phase.ToString();
    }
    public void GameOver()
    {
        
        float bestScore = PlayerPrefs.GetFloat("bestTime");

        if (surviveTime > bestScore)
        {
            bestScore = surviveTime;
            PlayerPrefs.SetFloat("bestTime", bestScore);
            PlayerPrefs.Save(); // ����� ���� ��� �ݿ�
        }

        bestTime.text = "Best Time: " + bestScore.ToString("0.00");
        gameOverUI.SetActive(true);

    }
}
