﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public GameController gameController;
    
    [SerializeField] private bool startToRight;
    [SerializeField] private GameObject disolveParticles;
    [SerializeField] public float StartingSpeed;
    [SerializeField] private float EatingTime;

    private float speed;
    private float maxLeftMovingPosition = -6.0f;
    private float maxRightMovingPosition = 6.0f;

    private bool canMove = false;
    private bool eatingAnimationIsActive = false;

    private Rigidbody2D myBody;

    private Animator Animator;

    void Start()
    {
        speed = StartingSpeed;
        myBody = GetComponent<Rigidbody2D>();
        speed = startToRight == true ? speed : speed * -1;

        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove == true && eatingAnimationIsActive == false)
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
        float yRotation = xPosition > 0 ? 0f : -180f;
        Quaternion rot = Quaternion.Euler(0f, yRotation, 0f);
        transform.rotation = rot;
        speed *= -1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Saw"))
        {
            Instantiate(disolveParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            gameController.SheepCollideWithSaw();
            Vector3 pos = transform.position;

            float SecondSheepXPosition = gameController.GetSecondSheepXPosition(gameObject.tag, pos.x);

            if(SecondSheepXPosition > 0)
            {
                SwitchMovingDirection(maxLeftMovingPosition);
            }
            else
            {
                SwitchMovingDirection(maxRightMovingPosition);
            }
        }
        else
        {// dodat uslov da bi se izbeglo da se dve ovce poklope
            if(col.transform.rotation.eulerAngles.y == transform.rotation.eulerAngles.y)
            {
                if(eatingAnimationIsActive == false)
                {
                    float yRotation = transform.rotation.eulerAngles.y == 0 ? -180f : 0f;
                    Quaternion rot = Quaternion.Euler(0f, yRotation, 0f);
                    transform.rotation = rot;
                    speed *= -1;
                }
            }
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

    public void SetCanMove(bool can)
    {
        canMove = can;
        Animator.enabled = can;
    }

    private void OnMouseDown()
    {
        if (gameController.CanPauseSheep())
        {
            eatingAnimationIsActive = true;
            Animator.SetBool("isEatingStart", true);
            StartCoroutine(StartWalking());
            gameController.StartPauseSheepLoadingAnimation();
        }
    }

    private IEnumerator StartWalking()
    {
        yield return new WaitForSeconds(EatingTime);
        Animator.SetBool("isEatingEnd", true);
        Animator.SetBool("isEatingStart", false);
    }

    // metoda se zove u eventu na poslednjem frejmu EatE animacije u Animation tabu
    public void EatingAnimationIsDone()
    {
        eatingAnimationIsActive = false;
        Animator.SetBool("isEatingEnd", false);
    }
}
