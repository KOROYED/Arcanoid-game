using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDestruction : MonoBehaviour
{
    float visibleHeightThreshold;
    // Start is called before the first frame update
    void Start()
    {
        visibleHeightThreshold = -Camera.main.orthographicSize - transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < visibleHeightThreshold)
            Destroy(gameObject);
    }
}
