using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStatus gameStatus;
    public int copCount = 0;
    public CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
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
    }

    void CheckGameConditions()
    {
        if (copCount < 0)
        {
            gameStatus = GameStatus.GAME_OVER;
        }
    }


    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject player = GameObject.Instantiate(PrefabManager.instance.PLAYER.gameObject, GameObject.Find("Starting Point").transform.position, Quaternion.identity);

        virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = player.transform;
        virtualCamera.UpdateCameraState(Vector3.up, 10f);
    }
}

public enum GameStatus
{
    INITIAL,
    GAME_COMPLETE,
    GAME_IN_PROGRESS,
    LEVEL_COMPLETE,
    GAME_OVER,
}