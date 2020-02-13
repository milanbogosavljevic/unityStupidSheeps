using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    float speed;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        if(GetComponent<SpriteRenderer>().flipX == true)
        {
            speed = -1.0f;
        }
        else
        {
            speed = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        position.x += speed * Time.deltaTime;
        transform.position = position;

        if(transform.position.x > 6)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            speed = -1.0f;
        }

        if (transform.position.x < -6)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            speed = 1.0f;
        }
    }
}
