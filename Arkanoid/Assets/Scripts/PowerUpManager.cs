using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    Player script;
    public int speedBuffDuration;


    void Start() 
    {
        script = GameObject.Find("Player").GetComponent<Player>();
        
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
    }



    internal void OnPlayerHitSpeedDeBuff()
    {

        script.currentSpeed /= 2;
        FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitSpeedDeBuff;
        StartCoroutine(playerSpeedBuffAndDebuffTimer());
    }
    internal void OnPlayerHitSpeedBuff()
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
}
