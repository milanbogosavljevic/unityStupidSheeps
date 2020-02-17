using UnityEngine;

public class Saw : MonoBehaviour
{

    //private Rigidbody2D myBody;
    [SerializeField] private float positionLeft;
    [SerializeField] private float positionRight;
    [SerializeField] private float positionY;

    private float rotationSpeed = 550f;

    
    void Start()
    {
        //myBody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    public void MoveSaw()
    {
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
