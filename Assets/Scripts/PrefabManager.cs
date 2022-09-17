using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;
    public Player PLAYER;

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

    // Update is called once per frame
    void Update()
    {

    }
}
