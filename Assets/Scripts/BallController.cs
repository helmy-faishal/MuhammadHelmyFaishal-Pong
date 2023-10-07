using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 7f;
    public bool useRandomY = true;
    public float minY = 0f;
    public float maxY = 1f;

    public GameObject lastHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetAndStartGame();
    }

    public void ResetAndStartGame()
    {
        rb.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        lastHit = null;
        StartCoroutine(StartMoveCoroutine());
    }

    IEnumerator StartMoveCoroutine()
    {
        // Delay 2 detik sebelum bola bergerak
        yield return new WaitForSeconds(2);

        Vector2 initDirection = Random.value > 0.5 ? Vector2.right : Vector2.left;

        if (useRandomY)
        {
            float yDirection = Random.value > 0.5 ? 1 : -1;

            initDirection.y = Random.Range(minY, maxY) * yDirection;
        }

        rb.velocity = initDirection * moveSpeed;

        Debug.Log($"Initial ball direction = {initDirection}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            lastHit = collision.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checker"))
        {
            AddScore(collision.gameObject);
        }
    }

    void AddScore(GameObject checker)
    {
        string loseSide = checker.name;

        Debug.Log($"{loseSide} Lose");

        bool isRightScoring = loseSide != "Right";

        GameManager.instance.AddScore(isRightScoring);

        ResetAndStartGame();
    }

    public void ApplyBuffSpeed(float buffSpeedMultiplier)
    {

        float oldVelocity = rb.velocity.magnitude;
        rb.velocity *= buffSpeedMultiplier;

        Debug.Log($"Buff Apllied, Velocity {oldVelocity} --> {rb.velocity.magnitude}");
    }

}
