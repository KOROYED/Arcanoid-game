using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event System.Action<Vector3> OnBallHitBlock;
    public int buffToSpawn = 0;

    public float speed = 6.0f;
    private Rigidbody2D rigidbody2D;
    float visibleHeightThreshold;
    public GameObject Player;
    Player player;

    private bool IsGameStarted = false;


    void Start()
    {
        visibleHeightThreshold = -Camera.main.orthographicSize - transform.localScale.y;
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();

    }
    private void Update()
    {
        //RestartSystem();
        if(OnBallHitBlock != null)
        {
            OnBallHitBlock(gameObject.transform.position);
        }

        //if (IsGameStarted == false && Input.GetKeyDown(KeyCode.Space))
        //{
        //    rigidbody2D.velocity = Vector2.up * speed;
        //    IsGameStarted = true;

        //}
    }

    
    float HitFactor (Vector2 ballPos, Vector2 playerPos, float playerWidth)
    {
        return (ballPos.x - playerPos.x) / playerWidth;
    }

    void RestartSystem()
    {
        if (IsGameStarted == false)
        {
            //transform.position = player.transform.position + new Vector3(0, 0.3f);
        }
        //if (IsGameStarted == false && Input.GetKeyDown(KeyCode.Space))
        //{
        //    rigidbody2D.velocity = Vector2.up * speed;
        //    IsGameStarted = true;

        //}
        //if (transform.position.y < visibleHeightThreshold)
        //{
        //    transform.position = player.transform.position + new Vector3(0, 0.3f);
        //    rigidbody2D.velocity = Vector2.zero;
        //    ballCount--;
        //    IsGameStarted = false;
        //}
        //if(ballCount == 0)
        //{
        //    Destroy(gameObject);
        //}
        
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
        if(collision.gameObject.tag == "Ball")
        {
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
        }
        if(collision.gameObject.tag == "Wall")
        {
            FindObjectOfType<AudioManager>().Play("BallHitWall");
            float x = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);

            //Vector2 dir = new Vector2(x, -1).normalized;

            //rigidbody2D.velocity = dir * speed;
        }
    }
}
