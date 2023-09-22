using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int rightScore,leftScore = 0;
    public int maxScore = 5;
    public TMP_Text scoreUI;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        SetScoreText();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MoveToMainMenu();
        }
    }

    void SetScoreText()
    {
        scoreUI.text = $"{leftScore} - {rightScore}";
    }

    public void MoveToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AddScore(bool isRightSide)
    {
        if (isRightSide)
        {
            rightScore++;
        }
        else
        {
            leftScore++;
        }

        SetScoreText();

        if (rightScore >= maxScore || leftScore >= maxScore)
        {
            MoveToMainMenu();
            Debug.Log("Game Finished");
        }
    }
}
