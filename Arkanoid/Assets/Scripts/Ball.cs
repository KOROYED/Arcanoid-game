using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 100.0f;
    private Rigidbody2D rigidbody2D;
    float visibleHeightThreshold;
    public GameObject Player;

    private bool IsGameStarted = false;
    public float ballCount;


    void Start()
    {
        visibleHeightThreshold = -Camera.main.orthographicSize - transform.localScale.y;
        rigidbody2D = transform.GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        RestartSystem();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            float x = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);

            Vector2 dir = new Vector2(x, 1).normalized;

            rigidbody2D.velocity = dir * speed;
        }
        if(collision.gameObject.tag == "Block")
        {
            Destroy(collision.gameObject);
        }
    }
    float HitFactor (Vector2 ballPos, Vector2 playerPos, float playerWidth)
    {
        return (ballPos.x - playerPos.x) / playerWidth;
    }

    void RestartSystem()
    {
        if (IsGameStarted == false)
        {
            transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 0.3f);
        }
        if (IsGameStarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.velocity = Vector2.up * speed;
            IsGameStarted = true;

        }
        if (transform.position.y < visibleHeightThreshold)
        {
            transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 0.3f);
            rigidbody2D.velocity = Vector2.zero;
            ballCount--;
            IsGameStarted = false;
        }
        if (ballCount == 0)
        {
            Destroy(gameObject);
            print("You lost");
        }
    }
}
