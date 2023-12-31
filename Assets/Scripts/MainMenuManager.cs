using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager menuManager;

    public Button playButton;
    public Button quitButton;
    public Button creditButton;

    private void Awake()
    {
        menuManager = this;
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        creditButton.onClick.AddListener(OpenCreditScene);
    }

    public void PlayGame()
    {
        Debug.Log("Created by Muhammad Helmy Faishal");
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenCreditScene()
    {
        SceneManager.LoadScene("Credit");
    }
}
