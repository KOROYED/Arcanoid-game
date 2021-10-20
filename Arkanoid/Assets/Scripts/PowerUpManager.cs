using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    Player script;
    Ball ball;

    public int speedBuffDuration;
    public int playerSizeBuffDuration;
    public GameObject multiBallRight;
    public GameObject multiBallLeft;


    int playerSizeBuffed = 0;
    int playerSizeDebuffed = 0;

    void Start() 
    {
        script = GameObject.Find("Player").GetComponent<Player>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();

    }
    void Update()
    {
        if (script.buff == 1)
        {
            FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitSpeedBuff;
            script.buff = 0;
        }
        else if (script.buff == 2)
        {
            FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitSpeedDeBuff;
            script.buff = 0;
        }
        else if (script.buff == 3)
        {
            FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitMultiBallBuff;
            script.buff = 0;
        }
        else if (script.buff == 4)
        {
            FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitPlayerSizeBuff;
            script.buff = 0;
        }
        else if (script.buff == 5)
        {
            FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitPlayerSizeDebuff;
            script.buff = 0;
        }
    }

    void OnPlayerHitPlayerSizeBuff()
    {
        playerSizeBuffed++;
        if (playerSizeBuffed == 1)
        {
            Vector3 temp = script.transform.localScale;
            temp.x *= 2;
            script.transform.localScale = temp;
        }
        FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitPlayerSizeBuff;
        StartCoroutine(playerSizeBuffTimer());

    }
    void OnPlayerHitPlayerSizeDebuff()
    {
        playerSizeDebuffed++;
        if(playerSizeDebuffed == 1)
        {
            Vector3 temp = script.transform.localScale;
            temp.x /= 2;
            script.transform.localScale = temp;
        }
        FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitPlayerSizeDebuff;
        StartCoroutine(playerSizeBuffTimer());

    }
    void OnPlayerHitMultiBallBuff()
    {
        Instantiate(multiBallRight, ball.transform.position, Quaternion.Euler(0, 0, 0));
        Instantiate(multiBallLeft, ball.transform.position, Quaternion.Euler(0, 0, 0));
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
        if(playerSizeBuffed == 1)
        {
            Vector3 temp = script.transform.localScale;
            temp.x /= 2;
            script.transform.localScale = temp;
            playerSizeBuffed--;
        }
        if(playerSizeDebuffed == 1)
        {
            Vector3 temp = script.transform.localScale;
            temp.x *= 2;
            script.transform.localScale = temp;
            playerSizeBuffed--;
        }
    }
}
