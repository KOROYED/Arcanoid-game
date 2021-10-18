using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    Ball ball;

    public GameObject speedBuffPrefab;
    public GameObject speedDebuffPrefab;
    GameObject buffPrefab;

    void Start()
    {
        ball = GameObject.Find("Ball").GetComponent<Ball>();
    }


    void Update()
    {
        if (ball.buffToSpawn == 1)
        {
            FindObjectOfType<Ball>().OnBallHitBlock += SpawnBuff;
            ball.buffToSpawn = 0;
        }
    }

    void SpawnBuff(Vector3 position)
    {
        Debug.Log("kek");
        Vector3 spawnPosition = position;
        int buff = Random.Range(1, 5);
        switch (buff)
        {
            case 1:
                buffPrefab = speedBuffPrefab;
                Instantiate(buffPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
                break;
            case 2:
                buffPrefab = speedDebuffPrefab;
                Instantiate(buffPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
                break;
            default:
                break;
        }
        
        FindObjectOfType<Ball>().OnBallHitBlock -= SpawnBuff;
    }
}
