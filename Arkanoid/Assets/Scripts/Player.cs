using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public event Action OnPlayerHitBuff;
    

    public float currentSpeed = 5f;
    public float standartSpeed = 5f;

    public int buff = 0;

    float screenHalfWidthInWorldUnits;

    void Start()
    {
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
    }


    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * currentSpeed;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);
        float halfPlayerWidth = transform.localScale.x / 2;

        if (OnPlayerHitBuff != null)
        {
            OnPlayerHitBuff();
        }

        if (transform.position.x < -screenHalfWidthInWorldUnits + halfPlayerWidth)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits + halfPlayerWidth, transform.position.y);
        }
        if (transform.position.x > screenHalfWidthInWorldUnits - halfPlayerWidth)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits - halfPlayerWidth, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "SpeedBuff":
                buff = 1;
                Destroy(collider.gameObject);
                break;
            case "SpeedDebuff":
                buff = 2;
                Destroy(collider.gameObject);
                break;
            case "MultiBallBuff":
                buff = 3;
                Destroy(collider.gameObject);
                break;
            case "PlayerSizeBuff":
                buff = 4;
                Destroy(collider.gameObject);
                break;
            case "PlayerSizeDebuff":
                buff = 5;
                Destroy(collider.gameObject);
                break;
            case "BallSizeBuff":
                buff = 6;
                Destroy(collider.gameObject);
                break;
            default:
                break;
        }
    }
}
