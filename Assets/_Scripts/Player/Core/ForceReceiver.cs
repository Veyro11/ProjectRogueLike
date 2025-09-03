using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Player player;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    public void AddForce(Vector2 force)
    {
        rb.velocity = new Vector2(0, rb.velocity.y + 1f);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
    }

    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}