using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Rigidbody2D myBody;
    public GameController gameController;
    private bool canMove = true;

    [SerializeField] private bool startToLeft;

    private float speed = 0.9f;
    private float maxLeftMovingPosition = -6.0f;
    private float maxRightMovingPosition = 6.0f;

    void Start()
    {
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

    public void setCanMove(bool can)
    {
        canMove = can;
    }
}
