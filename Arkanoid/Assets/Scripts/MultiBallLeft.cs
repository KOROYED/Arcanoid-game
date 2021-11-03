using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallLeft : MonoBehaviour
{
    public event System.Action<Vector3> OnBallHitBlock;
    public int buffToSpawn = 0;

    Ball ball;
    public float speed = 6.0f;
    private Rigidbody2D rigidbody2D;
    float visibleHeightThreshold;
    public GameObject Player;

    public float ballCount;


    void Start()
    {
        visibleHeightThreshold = -Camera.main.orthographicSize - transform.localScale.y;
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(-1,1).normalized * speed/2;
        ball = GameObject.Find("Ball").GetComponent<Ball>();

    }
    private void Update()
    {
        BallDestructionMethod();
        transform.localScale = ball.transform.localScale;
        if (OnBallHitBlock != null)
        {
            OnBallHitBlock(gameObject.transform.position);
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
            float x = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);

            Vector2 dir = new Vector2(x, 1).normalized;

            rigidbody2D.velocity = dir * speed;
        }
        if (collision.gameObject.tag == "Block")
        {
            buffToSpawn = 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Ball")
        {
            float x = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            Vector2 dir = new Vector2(x, 1).normalized;
            rigidbody2D.velocity = dir * speed;
        }
    }
    void BallDestructionMethod()
    {
        if (transform.position.y < visibleHeightThreshold)
        {
            Destroy(gameObject);
        }
    }
}
