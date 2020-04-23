using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Button RestartButton;

    public void RestartGame()
    {
        RestartButton.interactable = false;
        SceneManager.LoadScene(1);
    }
}
