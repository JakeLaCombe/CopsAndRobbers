using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject PauseText;
    private GameObject ResumeButton;
    private GameObject MenuButton;
    private GameObject RestartLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        PauseText = this.transform.Find("Text").gameObject;
        ResumeButton = this.transform.Find("Resume").gameObject;
        MenuButton = this.transform.Find("Menu").gameObject;
        RestartLevelButton = this.transform.Find("Restart Level").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        bool isPaused = Time.timeScale == 0.0f;

        PauseText.SetActive(isPaused);
        ResumeButton.SetActive(isPaused);
        MenuButton.SetActive(isPaused);
        RestartLevelButton.SetActive(isPaused);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
