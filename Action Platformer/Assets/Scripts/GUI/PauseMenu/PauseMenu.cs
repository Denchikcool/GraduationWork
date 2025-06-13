using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    public void PauseButtonClicked()
    {
        _pauseMenu.SetActive(true);
        MusicManager.PauseBackgroundMusic();
        Time.timeScale = 0.0f;
    }

    public void HomeButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    public void ResumeButtonClicked()
    {
        _pauseMenu.SetActive(false);
        MusicManager.PlayBackgroundMusic(false);
        Time.timeScale = 1.0f;
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
}
