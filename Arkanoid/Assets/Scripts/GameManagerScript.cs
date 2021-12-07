using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    Player player;
    Ball ball;
    float visibleHeightThreshold;
    private bool IsGameStarted = false;
    public int ballCount = 0;
    private bool gameOver = false;
    void Start()
    {
        visibleHeightThreshold = -Camera.main.orthographicSize - transform.localScale.y;
        player = GameObject.Find("Player").GetComponent<Player>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();
    }


    void Update()
    {
        if (IsGameStarted == false && FindObjectsOfType<Ball>().Length > 0)
        {
            ball.transform.position = player.transform.position + new Vector3(0, 0.3f);
        }
        if (ball.gameObject.activeInHierarchy == false && ballCount == 0 && gameOver == false && FindObjectsOfType<Multiball>().Length == 0)
        {
            FindObjectOfType<AudioManager>().Play("Gameover");
            gameOver = true;
            IsGameStarted = false;
            print("You lost");
        }
        if (IsGameStarted == false && Input.GetKeyDown(KeyCode.Space) && FindObjectsOfType<Ball>().Length > 0)
        {
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.up * ball.speed;
            IsGameStarted = true;

        }
        if (ball.transform.position.y < visibleHeightThreshold && FindObjectsOfType<Ball>().Length > 0 && FindObjectsOfType<Multiball>().Length == 0)
        {
            FindObjectOfType<AudioManager>().Play("Lifelose");
            ball.transform.position = player.transform.position + new Vector3(0, 0.3f);
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ballCount--;
            IsGameStarted = false;
        }
        if (ballCount == 0)
        {
            ball.gameObject.SetActive(false);
        }
        if(FindObjectsOfType<BlocksScript>().Length == 0 && gameOver == false)
        {
            FindObjectOfType<AudioManager>().Play("Win");
            gameOver = true;
        }
    }
}
