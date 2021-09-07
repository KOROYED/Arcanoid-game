using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    Player script;
    public int speedBuffDuration;
    private void Start()
    {
        script = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            script.speed = script.speed * 2;
            StartCoroutine(playerSpeedBuffTimer());
        }
    }

    IEnumerator playerSpeedBuffTimer()
    {
        yield return new WaitForSeconds(speedBuffDuration);
        playerSpeedBuffUndo();
    }
    void playerSpeedBuffUndo()
    {
        script.speed = script.speed / 2;
        Destroy(gameObject);
    }
}
