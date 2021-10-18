using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public event System.Action OnPlayerHitBuff;

    PowerUpManager powerUpManager;

    public float currentSpeed = 5f;
    public float standartSpeed = 5f;
    float screenHalfWidthInWorldUnits;

    void Start()
    {
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        powerUpManager = GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>();
    }


    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * currentSpeed;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);
        float halfPlayerWidth = transform.localScale.x / 2;


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
        if (collider.gameObject.tag == "SpeedBuff")
        {
            OnPlayerHitBuff += powerUpManager.OnPlayerHitSpeedBuff;

            if (OnPlayerHitBuff != null)
            {
                OnPlayerHitBuff();
                Destroy(collider.gameObject);
            }
        }
        if (collider.gameObject.tag == "SpeedDebuff")
        {
            OnPlayerHitBuff += powerUpManager.OnPlayerHitSpeedDeBuff;
            if (OnPlayerHitBuff != null)
            {
                OnPlayerHitBuff();
                Destroy(collider.gameObject);
            }
        }
    }
}
