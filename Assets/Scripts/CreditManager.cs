using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    public Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener(MoveToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MoveToMainMenu();
        }
    }

    void MoveToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
