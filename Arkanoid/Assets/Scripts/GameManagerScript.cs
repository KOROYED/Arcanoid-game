using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    Player player;
    Ball ball;
    float visibleHeightThreshold;
    private bool IsGameStarted = false;
    public int ballCount = 0;
    private bool gameOver = false;

    public static GameManagerScript instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
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
            AudioManager.instance.Play("Gameover");
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
            AudioManager.instance.Play("Lifelose");
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
            AudioManager.instance.Play("Win");
            LevelChanger();
            gameOver = true;
        }
    }


    IEnumerator LoadNewScene(string SceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneName);
    }

    void LevelChanger()
    {
        if(SceneManager.GetActiveScene().name == "Level 1")
        {
            StartCoroutine(LoadNewScene("Level 2", 1f));
        }
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            StartCoroutine(LoadNewScene("Level 1", 1f));
        }
    }
}
