using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject InfoObject;
    [SerializeField] private GameObject OptionsObject;
    void Start()
    {
        
    }

    public void ToggleInfoObjectVisibility()
    {
        if (OptionsObject.gameObject.activeSelf)
        {
            ToggleOptionsVisibility();
        }
        InfoObject.gameObject.SetActive(!InfoObject.gameObject.activeSelf);
    }

    public void ToggleOptionsVisibility()
    {
        if (InfoObject.gameObject.activeSelf)
        {
            ToggleInfoObjectVisibility();
        }
        OptionsObject.gameObject.SetActive(!OptionsObject.gameObject.activeSelf);
    }
}
