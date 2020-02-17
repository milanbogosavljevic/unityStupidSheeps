using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    /*    float speed;
        Vector3 position;*/

    private Rigidbody2D myBody;

    [SerializeField] private float speed;
    [SerializeField] private bool startToLeft;

    void Start()
    {
        /*        position = transform.position;

                if(GetComponent<SpriteRenderer>().flipX == true)
                {
                    speed = -1.0f;
                }
                else
                {
                    speed = 1.0f;
                }*/

        myBody = GetComponent<Rigidbody2D>();
        //if (GetComponent<SpriteRenderer>().flipX == true)
        if (startToLeft == true)
        {
            speed = -0.6f;
        }
        else
        {
            speed = 0.6f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*        position.x += speed * Time.deltaTime;
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
                }*/

        
        if (transform.position.x >= 6.0f)
        {
            //GetComponent<SpriteRenderer>().flipX = true;
            Quaternion rot = Quaternion.Euler(0f, -180f, 0f);
            this.transform.rotation = rot;
            speed = -0.6f;
        }

        if (transform.position.x <= -6.0f)
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
            this.transform.rotation = rot;
            speed = 0.6f;
        }
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Saw")
        {
            Debug.Log("Destroy Object");
        }
    }
}
