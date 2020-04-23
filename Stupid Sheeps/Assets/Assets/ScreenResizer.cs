using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResizer : MonoBehaviour
{
    public Camera MainCam;
    // Start is called before the first frame update
    void Start()
    {
        ScaleGame();
    }

    // Update is called once per frame
    private void ScaleGame()
    {
        Vector2 deviceScreenRes = new Vector2(Screen.width, Screen.height);
        Debug.Log(deviceScreenRes);

        float srcHeight = Screen.height;
        float srcWidth = Screen.width;

        float DeviceAspect = srcWidth / srcHeight;
        MainCam.aspect = DeviceAspect;
    }
}
