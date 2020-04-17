using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Button RestartButton;

    public void RestartGame()
    {
        //RestartButton.interactable = false;
        SceneManager.LoadScene(1);
    }
}
