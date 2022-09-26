using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BaseCanvas;
    private TextMeshProUGUI CopCount;
    private TextMeshProUGUI GameStatus;
    private TextMeshProUGUI RobberCount;
    private TextMeshProUGUI Timer;

    void Start()
    {
        CopCount = BaseCanvas.transform.Find("CopCount").GetComponent<TextMeshProUGUI>();
        GameStatus = BaseCanvas.transform.Find("GameStatus").GetComponent<TextMeshProUGUI>();
        RobberCount = BaseCanvas.transform.Find("RobberCount").GetComponent<TextMeshProUGUI>();
        Timer = BaseCanvas.transform.Find("Timer").GetComponent<TextMeshProUGUI>();
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
        }
        else if (gameManager.gameStatus == global::GameStatus.TRANSITIONING)
        {
            GameStatus.enabled = true;
            GameStatus.text = "Nice Job!";
        }
        else
        {
            GameStatus.enabled = false;
        }
    }
}
