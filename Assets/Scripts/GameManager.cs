using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStatus gameStatus;
    public int copCount = 0;
    public int robberCount = 10;
    public CinemachineVirtualCamera virtualCamera;
    public Coroutine restartGameCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        robberCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        gameStatus = GameStatus.GAME_IN_PROGRESS;
    }

    public void LoseLife()
    {
        if (copCount > 0)
        {
            copCount -= 1;
            StartCoroutine(RespawnCoroutine());
        }
        else
        {
            gameStatus = GameStatus.GAME_OVER;

            if (restartGameCoroutine == null)
            {
                restartGameCoroutine = StartCoroutine(RestartGame());
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (virtualCamera == null)
        {
            virtualCamera = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        }

        if (gameStatus == GameStatus.GAME_IN_PROGRESS)
        {
            CheckGameConditions();
        }

        if (gameStatus == GameStatus.LEVEL_COMPLETE)
        {
            gameStatus = GameStatus.TRANSITIONING;
            StartCoroutine(NextScene());
        }
    }

    void CheckGameConditions()
    {
        if (copCount < 0)
        {
            gameStatus = GameStatus.GAME_OVER;

            if (restartGameCoroutine == null)
            {
                restartGameCoroutine = StartCoroutine(RestartGame());
            }
        }

        if (robberCount <= 0)
        {
            gameStatus = GameStatus.LEVEL_COMPLETE;
        }
    }


    private IEnumerator RespawnCoroutine()
    {
        Debug.Log("Respawning");
        yield return new WaitForSeconds(3.0f);
        GameObject player = GameObject.Instantiate(PrefabManager.instance.PLAYER.gameObject, GameObject.Find("Starting Point").transform.position, Quaternion.identity);

        virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = player.transform;
        virtualCamera.UpdateCameraState(Vector3.up, 10f);
    }


    private IEnumerator RestartGame()
    {
        Debug.Log("Restarting");
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }
}

public enum GameStatus
{
    INITIAL,
    GAME_COMPLETE,
    GAME_IN_PROGRESS,
    LEVEL_COMPLETE,
    TRANSITIONING,
    GAME_OVER,
}