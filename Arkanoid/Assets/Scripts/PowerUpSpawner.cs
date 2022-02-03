using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public Vector3 position = Vector3.zero;
    public int buffToSpawn = 0;
    public event System.Action<Vector3> OnBallHitBlock;


    public GameObject multiBallPrefab;
    public GameObject speedBuffPrefab;
    public GameObject speedDebuffPrefab;
    public GameObject playerSizeBuff;
    public GameObject playerSizeDebuff;
    public GameObject ballSizeBuff;
    GameObject buffPrefab;

    public static PowerUpSpawner instance;
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
    }


    void Update()
    {
        if (OnBallHitBlock != null)
        {
            OnBallHitBlock(position);
        }
        if (buffToSpawn == 1)
        {
            OnBallHitBlock += SpawnBuff;
            buffToSpawn = 0;
        }
    }

    void SpawnBuff(Vector3 position)
    {
        Vector3 spawnPosition = position;
        int buff = Random.Range(1, 24);
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
        
        OnBallHitBlock -= SpawnBuff;
    }
}
