using UnityEngine;

public class SpeedFinger : MonoBehaviour
{
    private float fingerSpeed = 90f;
    private float rotationSpeed;
    private float previousSpeed = 0f;
    private bool RotateFinger = false;

    // Update is called once per frame
    void Update()
    {
        if (RotateFinger)
        {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            Debug.Log(gameObject.transform.localEulerAngles.z);
        }
    }

    public void SetFingerSpeed(string speed)
    {
        Debug.Log("speed" + speed);
        float speedDivider = float.Parse(speed) - previousSpeed;
        rotationSpeed = fingerSpeed / speedDivider;
        previousSpeed = float.Parse(speed);
        Debug.Log("divider " + speedDivider);
    }

    public void RunAnimation(bool run)
    {
        RotateFinger = run;
    }
}
