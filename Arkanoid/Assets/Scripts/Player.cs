using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    float screenHalfWidthInWorldUnits;

    void Start()
    {
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
    }


    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * speed;
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

    
}
