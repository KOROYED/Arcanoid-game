using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 100.0f;
    private Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = Vector2.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            float x = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);

            Vector2 dir = new Vector2(x, 1).normalized;

            rigidbody2D.velocity = dir * speed;
        }
    }
    float HitFactor (Vector2 ballPos, Vector2 playerPos, float playerWidth)
    {
        return (ballPos.x - playerPos.x) / playerWidth;
    }
}
