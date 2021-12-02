using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiball : MonoBehaviour
{
    Ball ball;
    public event System.Action<Vector3> OnBallHitBlock;
    public int buffToSpawn = 0;

    public float speed = 6.0f;
    private Rigidbody2D rigidbody2D;
    float visibleHeightThreshold;

    private bool IsGameStarted = false;
    public float ballCount;


    void Start()
    {
        visibleHeightThreshold = -Camera.main.orthographicSize - transform.localScale.y;
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();
    }
    private void Update()
    {
        if (transform.position.y < visibleHeightThreshold)
        {
            Destroy(gameObject);
        }
        if (OnBallHitBlock != null)
        {
            OnBallHitBlock(gameObject.transform.position);
        }
        if (ball != null)
        {
            gameObject.transform.localScale = ball.transform.localScale;
        }
        
    }


    float HitFactor(Vector2 ballPos, Vector2 playerPos, float playerWidth)
    {
        return (ballPos.x - playerPos.x) / playerWidth;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            FindObjectOfType<AudioManager>().Play("BallHitWall");
            float x = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);

            Vector2 dir = new Vector2(x, 1).normalized;

            rigidbody2D.velocity = dir * speed;
        }
        if (collision.gameObject.tag == "Block")
        {
            FindObjectOfType<AudioManager>().Play("BlockBreak");
            buffToSpawn = 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Ball")
        {
            //    float x = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            //    Vector2 dir = new Vector2(x, 1).normalized;
            //    rigidbody2D.velocity = dir * speed;
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
        }
        if (collision.gameObject.tag == "Wall")
        {
            FindObjectOfType<AudioManager>().Play("BallHitWall");
        }
    }
}

