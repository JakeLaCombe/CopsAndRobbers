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

    void Start()
    {
        CopCount = BaseCanvas.transform.Find("CopCount").GetComponent<TextMeshProUGUI>();
        GameStatus = BaseCanvas.transform.Find("GameStatus").GetComponent<TextMeshProUGUI>();
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

        if (gameManager.gameStatus == global::GameStatus.GAME_OVER)
        {
            GameStatus.enabled = true;
            GameStatus.text = "Game Over";
        }
        else
        {
            GameStatus.enabled = false;
        }
    }
}
