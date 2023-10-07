using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public KeyCode keyUp = KeyCode.W;
    public KeyCode keyDown = KeyCode.S;
    public float moveSpeed = 5f;

    Vector3 originalScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 move = Vector2.zero;
        if (Input.GetKey(keyUp))
        {
            move = Vector2.up;
        }
        else if (Input.GetKey(keyDown))
        {
            move = Vector2.down;
        }

        rb.velocity = move * moveSpeed;

        Debug.Log($"{gameObject.name} velocity = {rb.velocity.magnitude}");
    }

    public void ApplySpeedBuff(float speedMultiplier,float buffTime)
    {
        StartCoroutine(SpeedBuffCoroutine(speedMultiplier,buffTime));
    }

    IEnumerator SpeedBuffCoroutine(float speedMultiplier, float buffTime)
    {
        Debug.Log($"Buff speed applied to {gameObject.name}");
        moveSpeed *= speedMultiplier;
        yield return new WaitForSeconds(buffTime);
        moveSpeed /= speedMultiplier;
        Debug.Log("Buff end!");
    }

    public void ApplyScaleBuff(float scaleMultiplier, float buffTime)
    {
        StartCoroutine(ScaleBuffCoroutine(scaleMultiplier,buffTime));
    }

    IEnumerator ScaleBuffCoroutine(float scaleMultiplier, float buffTime)
    {
        Debug.Log($"Buff scale applied to {gameObject.name}");

        // Buff scale pada Y
        Vector3 newScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y * scaleMultiplier,
            transform.localScale.z);

        transform.localScale = newScale;
        yield return new WaitForSeconds(buffTime);
        transform.localScale = originalScale;
        Debug.Log("Buff end!");
    }
}
