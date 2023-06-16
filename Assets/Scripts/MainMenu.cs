using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playButton, creditsButton, quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(Play);
        creditsButton.onClick.AddListener(DisplayCredits);
        quitButton.onClick.AddListener(Quit);
    }

    private void Play()
    {
        SceneManager.LoadScene("Game");
    }

    private void DisplayCredits()
    {
        SceneManager.LoadScene("Intro");
    }

    private void Quit()
    {
        Application.Quit();
    }
}
