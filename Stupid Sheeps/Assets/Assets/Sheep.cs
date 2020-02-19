using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Rigidbody2D myBody;
    public GameController gameController;
    public bool canMove = true;

    [SerializeField] private float speed;
    [SerializeField] private bool startToLeft;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        if (startToLeft == true)
        {
            speed = -0.6f;
        }
        else
        {
            speed = 0.6f;
        }
    }

    void Update()
    {
        if(canMove == false)
        {
            return;
        }
        if (transform.position.x >= 6.0f)
        {
            Quaternion rot = Quaternion.Euler(0f, -180f, 0f);
            this.transform.rotation = rot;
            speed = -0.6f;
        }

        if (transform.position.x <= -6.0f)
        {
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
            gameObject.SetActive(false);
            gameController.SheepCollideWithSaw();
        }
    }

    public void setCanMove(bool can)
    {
        Debug.Log("set can move");
        canMove = can;
    }
}
