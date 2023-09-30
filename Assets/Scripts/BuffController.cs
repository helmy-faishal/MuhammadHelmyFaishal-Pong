using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public string buffType = "speed";
    public float buffSpeedMultiplier = 1.5f;
    public float defaultBuffDestroyer = 5f;

    private void Start()
    {
        SetBuff(buffType, defaultBuffDestroyer);
    }

    public void SetBuff(string buffType, float destroyInterval)
    {
        this.buffType = buffType;
        DestroyBuff(destroyInterval);
    }
    public void DestroyBuff(float destroyInterval)
    {
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
            collision.gameObject.GetComponent<BallController>().ApplyBuffSpeed(buffSpeedMultiplier);
            DestroyBuff(0f);
        }
    }
}
