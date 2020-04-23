using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject InfoObject;
    [SerializeField] private GameObject OptionsObject;
    [SerializeField] private Button PlayButton;

    private AudioSource click;
    void Start()
    {
        click = GetComponent<AudioSource>();
    }

    public void ToggleInfoObjectVisibility()
    {
        click.Play();
        if (OptionsObject.gameObject.activeSelf)
        {
            ToggleOptionsVisibility();
        }
        InfoObject.gameObject.SetActive(!InfoObject.gameObject.activeSelf);
    }

    public void ToggleOptionsVisibility()
    {
        click.Play();
        if (InfoObject.gameObject.activeSelf)
        {
            ToggleInfoObjectVisibility();
        }
        OptionsObject.gameObject.SetActive(!OptionsObject.gameObject.activeSelf);
    }

    public void PlayGame()
    {
        click.Play();
        PlayButton.interactable = false;
        SceneManager.LoadScene(1);
    }
}
