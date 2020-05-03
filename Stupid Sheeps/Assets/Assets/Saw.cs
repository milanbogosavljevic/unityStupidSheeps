using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Saw : MonoBehaviour
{

    //private Rigidbody2D myBody;
    [SerializeField] private float positionLeft;
    [SerializeField] private float positionRight;
    [SerializeField] private float positionY;
    [SerializeField] private SoundsController SoundsController;

    private const float RotationSpeed = 550f;

    private Collider2D SawCollider;

    private void Start()
    {
        SawCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(SawCollider.enabled)
        {
            transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
        }
    }

    public void PauseSaw(bool pause)
    {
        SoundsController.PlayClick();
        GetComponent<Collider2D>().enabled = !pause;
    }

    public void MoveSaw()
    {
        SoundsController.PlayClick();
        if (transform.position.x == positionLeft)
        {
            transform.position = new Vector2(positionRight, positionY);        
        }
        else
        {
            transform.position = new Vector2(positionLeft, positionY);
        }
    }
}
