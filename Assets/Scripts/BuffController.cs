using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    [Header("Buff Settings")]
    public float defaultBuffDestroyer = 5f;
    public float buffTime = 5f;

    public Dictionary<string, Color> buffColor = new Dictionary<string, Color>()
    {
        { "ball speed",Color.blue },
        { "paddle speed",Color.green },
        { "paddle scale",Color.red },
    };

    [Header("Buff")]
    public string buffType = "ball speed";
    public float ballSpeedMultiplier = 1.5f;
    public float paddleSpeedMultiplier = 2f;
    public float paddleScaleMultiplier = 2f;

    Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    private void Start()
    {
        SetBuff(buffType, defaultBuffDestroyer);
    }

    public void SetBuff(string buffType, float destroyInterval)
    {
        this.buffType = buffType;

        Color color = buffColor.GetValueOrDefault(buffType,Color.white);
        render.material.color = color;

        DestroyBuff(destroyInterval);
    }
    public void DestroyBuff(float destroyInterval)
    {
        StopAllCoroutines();
        StartCoroutine(DestroyBuffCoroutine(destroyInterval));
    }

    IEnumerator DestroyBuffCoroutine(float destroyInterval)
    {
        yield return new WaitForSeconds(destroyInterval);

        // (?.) Check null untuk di Scene Test Buff
        GameManager.instance?.DecreaseBuffNumber();

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ApplyBuff(collision.gameObject);
        }
    }

    void ApplyBuff(GameObject ball)
    {
        GameObject paddle = ball.GetComponent<BallController>().lastHit;
        switch (buffType)
        {
            case "ball speed":
                ball.GetComponent<BallController>().ApplyBuffSpeed(ballSpeedMultiplier);
                break;
            
            // (?.) Cek null saat bola belum menyentuh paddle
            case "paddle speed":
                paddle?.GetComponent<PlayerController>().ApplySpeedBuff(paddleSpeedMultiplier,buffTime);
                break;

            case "paddle scale":
                paddle?.GetComponent<PlayerController>().ApplyScaleBuff(paddleScaleMultiplier,buffTime);
                break;

            default: break;
        }

        DestroyBuff(0f);
    }
}
