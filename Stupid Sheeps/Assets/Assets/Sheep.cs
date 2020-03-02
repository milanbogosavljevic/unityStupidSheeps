using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Rigidbody2D myBody;
    public GameController gameController;
    private bool canMove = false;

    [SerializeField] private bool startToLeft;

    [SerializeField] private float StartingSpeed;
    private float speed;
    private float maxLeftMovingPosition = -6.0f;
    private float maxRightMovingPosition = 6.0f;

    void Start()
    {
        speed = StartingSpeed;
        myBody = GetComponent<Rigidbody2D>();
        speed = startToLeft == true ? speed * -1 : speed;
    }

    void Update()
    {
        if (canMove == true)
        {
            myBody.velocity = new Vector2(speed, 0f);
        }
        else
        {
            myBody.velocity = new Vector2(0f, 0f);
        }

        if (transform.position.x > maxRightMovingPosition)
        {
            this.SwitchMovingDirection(maxRightMovingPosition);
        }

        if (transform.position.x < maxLeftMovingPosition)
        {
            this.SwitchMovingDirection(maxLeftMovingPosition);
        }
    }

    void SwitchMovingDirection(float xPosition)
    {
        Vector3 pos = transform.position;
        pos.x = xPosition;
        transform.position = pos;
        float yRotation = xPosition > 0 ? -180f : 0f;
        Quaternion rot = Quaternion.Euler(0f, yRotation, 0f);
        transform.rotation = rot;
        speed *= -1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Saw")
        {
            gameObject.SetActive(false);
            gameController.SheepCollideWithSaw();
        }
    }

    public void SetSpeed(float newSpeed)
    {
        if(this.speed > 0)
        {
            this.speed = newSpeed;
        }
        else
        {
            this.speed = (newSpeed * -1);
        }
    }

    public void setCanMove(bool can)
    {
        canMove = can;
    }
}
