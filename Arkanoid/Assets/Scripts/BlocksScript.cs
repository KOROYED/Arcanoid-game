using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            int PowerUpChance = Random.Range(1, 101);
            print(PowerUpChance);
            PowerUpChance = 0;
        }
    }
}
