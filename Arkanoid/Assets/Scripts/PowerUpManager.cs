using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    Player script;
    Ball ball;

    public int speedBuffDuration;
    public int playerSizeBuffDuration;
    public int ballSizeBuffDuration;
    public GameObject ballObject;

    bool playerSizeBuffed = false;
    bool playerSizeDebuffed = false;
    bool ballSizeBuffed = false;

    public static PowerUpManager instance;
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
        script = GameObject.Find("Player").GetComponent<Player>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();

    }
    void Update()
    {
        switch (script.buff)
        {
            case 1:
                FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitSpeedBuff;
                script.buff = 0;
                break;
            case 2:
                FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitSpeedDeBuff;
                script.buff = 0;
                break;
            case 3:
                FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitMultiBallBuff;
                script.buff = 0;
                break;
            case 4:
                FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitPlayerSizeBuff;
                script.buff = 0;
                break;
            case 5:
                FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitPlayerSizeDebuff;
                script.buff = 0;
                break;
            case 6:
                FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitBallSizeBuff;
                script.buff = 0;
                break;
            default:
                break;
        }
    }
    void OnPlayerHitBallSizeBuff()
    {
        if (ballSizeBuffed == false)
        {
            Vector3 temp = ball.transform.localScale;
            temp *= 2;
            ball.transform.localScale = temp;
            ballSizeBuffed = true;
            StartCoroutine(ballSizeBuffTimer());
        }
        FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitBallSizeBuff;
    }
    void OnPlayerHitPlayerSizeBuff()
    {
        if (playerSizeBuffed == false)
        {
            Vector3 temp = script.transform.localScale;
            temp.x *= 2;
            script.transform.localScale = temp;
            playerSizeBuffed = true;
            StartCoroutine(playerSizeBuffTimer());
        }

        FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitPlayerSizeBuff;

    }
    void OnPlayerHitPlayerSizeDebuff()
    {
        if (playerSizeDebuffed == false)
        {
            Vector3 temp = script.transform.localScale;
            temp.x /= 2;
            script.transform.localScale = temp;
            playerSizeDebuffed = true;
            StartCoroutine(playerSizeBuffTimer());
        }

        FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitPlayerSizeDebuff;
        

    }
    void OnPlayerHitMultiBallBuff()
    {
        GameObject leftBall = Instantiate(ballObject, script.transform.position + Vector3.right, Quaternion.Euler(0, 0, 0));
        GameObject rightBall = Instantiate(ballObject, script.transform.position + Vector3.left, Quaternion.Euler(0, 0, 0));
        Rigidbody2D leftBallRg = leftBall.GetComponent<Rigidbody2D>();
        Rigidbody2D rightBallRg = rightBall.GetComponent<Rigidbody2D>();
        leftBallRg.velocity = ball.speed * new Vector2(1, 1).normalized;
        rightBallRg.velocity = ball.speed * new Vector2(-1, 1).normalized;
        FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitMultiBallBuff;
    }

    void OnPlayerHitSpeedDeBuff()
    {

        script.currentSpeed /= 2;
        FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitSpeedDeBuff;
        StartCoroutine(playerSpeedBuffAndDebuffTimer());
    }
    void OnPlayerHitSpeedBuff()
    {
        script.currentSpeed *= 2;
        FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitSpeedBuff;
        StartCoroutine(playerSpeedBuffAndDebuffTimer());
    }
    
    IEnumerator playerSpeedBuffAndDebuffTimer()
    {
        yield return new WaitForSeconds(speedBuffDuration);
        playerSpeedBuffUndo();
    }
    IEnumerator playerSizeBuffTimer()
    {
        yield return new WaitForSeconds(playerSizeBuffDuration);
        playerSizeBuffUndo();
    }
    IEnumerator ballSizeBuffTimer()
    {
        yield return new WaitForSeconds(ballSizeBuffDuration);
        ballSizeBuffUndo();
    }
    void ballSizeBuffUndo()
    {
        if (ballSizeBuffed == true)
        {
            Vector3 temp = ball.transform.localScale;
            temp /= 2;
            ball.transform.localScale = temp;
            ballSizeBuffed = false;
        }
    }
    void playerSpeedBuffUndo()
    {
        if(script.currentSpeed < script.standartSpeed)
        {
            script.currentSpeed *= 2;
            FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitSpeedDeBuff;
        }
        if (script.currentSpeed > script.standartSpeed)
        {
            script.currentSpeed /= 2;

            FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitSpeedBuff;
        }
    }
    void playerSizeBuffUndo()
    {
        if(playerSizeBuffed == true)
        {
            Vector3 temp = script.transform.localScale;
            temp.x /= 2;
            script.transform.localScale = temp;
            playerSizeBuffed = false;
        }
        if(playerSizeDebuffed == true)
        {
            Vector3 temp = script.transform.localScale;
            temp.x *= 2;
            script.transform.localScale = temp;
            playerSizeDebuffed = false;
        }
    }
}
