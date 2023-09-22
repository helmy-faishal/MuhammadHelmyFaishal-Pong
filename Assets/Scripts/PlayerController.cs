using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public KeyCode keyUp = KeyCode.W;
    public KeyCode keyDown = KeyCode.S;
    public float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
