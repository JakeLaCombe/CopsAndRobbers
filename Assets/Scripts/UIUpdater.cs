using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BaseCanvas;
    private TextMeshProUGUI CopCount;
    private TextMeshProUGUI GameStatus;
    private TextMeshProUGUI RobberCount;
    private TextMeshProUGUI Timer;
    private GameObject RestartLevelButton;
    private GameObject RestartGameButton;

    void Start()
    {
        CopCount = BaseCanvas.transform.Find("CopCount").GetComponent<TextMeshProUGUI>();
        GameStatus = BaseCanvas.transform.Find("GameStatus").GetComponent<TextMeshProUGUI>();
        RobberCount = BaseCanvas.transform.Find("RobberCount").GetComponent<TextMeshProUGUI>();
        Timer = BaseCanvas.transform.Find("Timer").GetComponent<TextMeshProUGUI>();
        RestartLevelButton = BaseCanvas.transform.Find("Restart Level").gameObject;
        RestartGameButton = BaseCanvas.transform.Find("Restart Game").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance)
        {
            UpdateDisplay(GameManager.instance);
        }
    }

    private void UpdateDisplay(GameManager gameManager)
    {
        CopCount.text = "x " + gameManager.copCount.ToString();
        RobberCount.text = "x " + gameManager.robberCount.ToString();
        Timer.text = "Time: " + Mathf.CeilToInt(gameManager.secondsRemaining);

        if (gameManager.gameStatus == global::GameStatus.GAME_OVER)
        {
            GameStatus.enabled = true;
            GameStatus.text = "Game Over";
            RestartLevelButton.SetActive(true);
            RestartGameButton.SetActive(true);
        }
        else if (gameManager.gameStatus == global::GameStatus.TRANSITIONING)
        {
            GameStatus.enabled = true;
            GameStatus.text = "Nice Job!";
            RestartLevelButton.SetActive(false);
            RestartGameButton.SetActive(false);
        }
        else
        {
            RestartLevelButton.SetActive(false);
            RestartGameButton.SetActive(false);
            GameStatus.enabled = false;
        }
    }
}
