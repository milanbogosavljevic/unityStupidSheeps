using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject InfoObject;
    [SerializeField] private GameObject OptionsObject;
    [SerializeField] private Button PlayButton;
    [SerializeField] private GameObject SoundButtonSelector;
    [SerializeField] private GameObject MusicButtonSelector;

    private AudioSource click;
    void Start()
    {
        click = GetComponent<AudioSource>();
        CheckIfMusicIsOn();
        CheckIfSoundIsOn();
    }

    private void CheckIfMusicIsOn()
    {
        if (PlayerPrefs.HasKey("MusicPlay") == false)
        {
            PlayerPrefs.SetString("MusicPlay", "on");
        }
        else
        {
            bool MusicIsOn = PlayerPrefs.GetString("MusicPlay") == "on" ? true : false;
            SetMusicInGame(MusicIsOn);
        }
    }

    private void CheckIfSoundIsOn()
    {
        if (PlayerPrefs.HasKey("SoundPlay") == false)
        {
            PlayerPrefs.SetString("SoundPlay", "on");
        }
        else
        {
            bool SoundIsOn = PlayerPrefs.GetString("SoundPlay") == "on" ? true : false;
            SetSoundInGame(SoundIsOn);
        }
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

    public void SetSoundInGame(bool on)
    {
        float xP = on == true ? -0.66f : 0.72f;
        SoundButtonSelector.transform.position = new Vector2(xP, SoundButtonSelector.transform.position.y);
        string SoundPlay = on == true ? "on" : "off";
        PlayerPrefs.SetString("SoundPlay", SoundPlay);
    }

    public void SetMusicInGame(bool on)
    {
        float xP = on == true ? -0.66f : 0.72f;
        MusicButtonSelector.transform.position = new Vector2(xP, MusicButtonSelector.transform.position.y);
        string MusicPlay = on == true ? "on" : "off";
        PlayerPrefs.SetString("MusicPlay", MusicPlay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
