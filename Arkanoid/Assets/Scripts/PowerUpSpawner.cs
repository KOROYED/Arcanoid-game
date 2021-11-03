using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    Ball ball;

    public GameObject multiBallPrefab;
    public GameObject speedBuffPrefab;
    public GameObject speedDebuffPrefab;
    public GameObject playerSizeBuff;
    public GameObject playerSizeDebuff;
    public GameObject ballSizeBuff;
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
        Vector3 spawnPosition = position;
        int buff = Random.Range(1, 12);
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
            case 3:
                buffPrefab = multiBallPrefab;
                Instantiate(buffPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
                break;
            case 4:
                buffPrefab = playerSizeBuff;
                Instantiate(buffPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
                break;
            case 5:
                buffPrefab = playerSizeDebuff;
                Instantiate(buffPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
                break;
            case 6:
                buffPrefab = ballSizeBuff;
                Instantiate(buffPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
                break;
            default:
                break;
        }
        
        FindObjectOfType<Ball>().OnBallHitBlock -= SpawnBuff;
    }
}
