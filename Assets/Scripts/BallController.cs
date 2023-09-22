using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 7f;
    public float minY = 0f;
    public float maxY = 1f;

    public bool useRandomY = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        rb.position = Vector2.zero;
        rb.velocity = Vector2.zero;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checker"))
        {
            string loseSide = collision.gameObject.name;

            Debug.Log($"{loseSide} Lose");

            bool isRightScoring = true;

            if (loseSide == "Right")
            {
                isRightScoring = false;
            }
            GameManager.instance.AddScore(isRightScoring);

            StartGame();
        }
    }

    
}
