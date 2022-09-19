using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryGuard : MonoBehaviour, IPlayerActionable
{
    // Start is called before the first frame update
    public AssistantDirections initialDirection = AssistantDirections.LEFT;
    private GameObject GuardRadar;

    private Vector2[] radarPoints = {
        new Vector2(-1.0f, 0.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(0.0f, -1.0f)
    };

    private float[] radarRotations = {
        180, 0, 90, 270
    };

    void Start()
    {
        GuardRadar = this.transform.Find("GuardRadar").gameObject;

        GuardRadar.transform.localPosition = new Vector3(radarPoints[(int)initialDirection].x, radarPoints[(int)initialDirection].y, 0.0f);
        GuardRadar.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, radarRotations[(int)initialDirection]);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerAction(Player player)
    {
        if (initialDirection == AssistantDirections.LEFT)
        {
            initialDirection = AssistantDirections.UP;
        }
        else if (initialDirection == AssistantDirections.UP)
        {
            initialDirection = AssistantDirections.RIGHT;
        }
        else if (initialDirection == AssistantDirections.RIGHT)
        {
            initialDirection = AssistantDirections.DOWN;
        }
        else if (initialDirection == AssistantDirections.DOWN)
        {
            initialDirection = AssistantDirections.LEFT;
        }

        GuardRadar.transform.localPosition = new Vector3(radarPoints[(int)initialDirection].x, radarPoints[(int)initialDirection].y, 0.0f);
        GuardRadar.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, radarRotations[(int)initialDirection]);
    }
}

public enum AssistantDirections
{
    LEFT = 0,
    RIGHT = 1,
    UP = 2,
    DOWN = 3,

}