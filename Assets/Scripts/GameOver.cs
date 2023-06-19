using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public Button replayButton, mainMenuButton, quitButton;
    public TMP_Text scoreText;

    void Start()
    {
        scoreText.text = "Final score : " + PlayerStats.score.ToString();

        replayButton.onClick.AddListener(Play);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        quitButton.onClick.AddListener(Quit);
    }

    private void Play()
    {
        SceneManager.LoadScene("Game");
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Quit()
    {
        Application.Quit();
    }
}
