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

    public List<string> buffTypeList = new List<string>{"speed"};

    public GameObject buffPrefabs;
    public Transform buffSpawner;
    public float minSpawnBuffInterval = 1f;
    public float maxSpawnBuffInterval = 5f;

    public float destroyBuffInterval = 3f;
    public int maxBuff = 2;
    public int  numberOfBuff;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        SetScoreText();
    }

    private void Start()
    {
        StartCoroutine(SpawnBuffCoroutine());
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

    public void DecreaseBuffNumber()
    {
        numberOfBuff--;
    }

    void SpawnBuff()
    {
        float offsetX = Random.Range(-6f, 6f);
        float offsetY = Random.Range(-3f, 3f);

        Vector3 offset = new Vector3(offsetX, offsetY);

        GameObject buff = Instantiate(buffPrefabs, buffSpawner.position + offset, Quaternion.identity);

        int index = Random.Range(0, buffTypeList.Count);

        string buffType = buffTypeList[index];
        buff.GetComponent<BuffController>().SetBuff(buffType, destroyBuffInterval);
    }

    IEnumerator SpawnBuffCoroutine()
    {
        
        float interval = Random.Range(minSpawnBuffInterval, maxSpawnBuffInterval);
        yield return new WaitForSeconds(interval);

        if (numberOfBuff < maxBuff)
        {
            SpawnBuff();
            numberOfBuff += 1;
        }

        StartCoroutine(SpawnBuffCoroutine());
    }
}
