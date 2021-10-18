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

    internal void OnPlayerHitSpeedDeBuff()
    {
        script.currentSpeed /= 2;
        StartCoroutine(playerSpeedBuffAndDebuffTimer());
    }
    internal void OnPlayerHitSpeedBuff()
    {
        script.currentSpeed *= 2;
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
            FindObjectOfType<Player>().OnPlayerHitBuff += OnPlayerHitSpeedDeBuff;
        }
        if (script.currentSpeed > script.standartSpeed)
        {
            script.currentSpeed /= 2;

            FindObjectOfType<Player>().OnPlayerHitBuff -= OnPlayerHitSpeedBuff;
        }
    }
}
