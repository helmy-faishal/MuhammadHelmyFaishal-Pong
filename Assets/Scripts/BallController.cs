using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 initDirection = Vector2.left;
        if (Random.value > 0.5)
        {
            initDirection = Vector2.right;
        }
        rb.velocity = initDirection * moveSpeed;
    }
}