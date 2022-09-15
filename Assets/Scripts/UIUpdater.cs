using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BaseCanvas;
    private TextMeshProUGUI CopCount;
    void Start()
    {
        CopCount = BaseCanvas.transform.Find("CopCount").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance)
        {
            CopCount.text = "x " + GameManager.instance.copCount.ToString();
        }
    }
}
