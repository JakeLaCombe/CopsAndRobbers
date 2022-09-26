using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance;
    public AudioSource SMOKE;
    public AudioSource ROTATE;

    public AudioSource WHISTLE;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            SMOKE = this.transform.Find("Smoke").GetComponent<AudioSource>();
            ROTATE = this.transform.Find("Rotate").GetComponent<AudioSource>();
            WHISTLE = this.transform.Find("Whistle").GetComponent<AudioSource>();
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
