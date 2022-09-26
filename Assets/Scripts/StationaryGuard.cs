using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryGuard : MonoBehaviour, IPlayerActionable
{
    // Start is called before the first frame update
    public AssistantDirections initialDirection = AssistantDirections.LEFT;
    private GameObject GuardRadar;

    private Animator animator;

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
        GuardRadar = this.transform.parent.Find("GuardRadar").gameObject;

        GuardRadar.transform.localPosition = new Vector3(radarPoints[(int)initialDirection].x, radarPoints[(int)initialDirection].y, 0.0f);
        GuardRadar.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, radarRotations[(int)initialDirection]);

        animator = GetComponent<Animator>();

        if (initialDirection == AssistantDirections.LEFT)
        {
            this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerAction(Player player)
    {
        SoundManager.instance.ROTATE.Play();

        if (initialDirection == AssistantDirections.LEFT)
        {
            initialDirection = AssistantDirections.UP;

            animator.SetBool("isFacingUp", true);
            animator.SetBool("isFacingDown", false);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (initialDirection == AssistantDirections.UP)
        {
            initialDirection = AssistantDirections.RIGHT;

            animator.SetBool("isFacingUp", false);
            animator.SetBool("isFacingDown", false);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (initialDirection == AssistantDirections.RIGHT)
        {
            initialDirection = AssistantDirections.DOWN;

            animator.SetBool("isFacingUp", false);
            animator.SetBool("isFacingDown", true);
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (initialDirection == AssistantDirections.DOWN)
        {
            initialDirection = AssistantDirections.LEFT;

            animator.SetBool("isFacingUp", false);
            animator.SetBool("isFacingDown", false);
            this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        GuardRadar.transform.localPosition = new Vector3(radarPoints[(int)initialDirection].x, radarPoints[(int)initialDirection].y, 0.0f);
        GuardRadar.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, radarRotations[(int)initialDirection]);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyRadar")
        {
            GameObject.Instantiate(PrefabManager.instance.SMOKE, this.transform.parent.transform.position, Quaternion.identity);
            Destroy(this.transform.parent.gameObject);
        }
    }
}

public enum AssistantDirections
{
    LEFT = 0,
    RIGHT = 1,
    UP = 2,
    DOWN = 3,

}