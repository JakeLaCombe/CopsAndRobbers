using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMoveState : IState
{
    Player player;

    private Coroutine WhistleReset;

    private Vector2[] detectionPoints = {
        new Vector2(-0.5f, 0.0f),
        new Vector2(0.5f, 0.0f),
        new Vector2(0.0f, 0.5f),
        new Vector2(0.0f, -0.5f)
    };

    public PlayerMoveState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {

    }
    public void Execute()
    {
        float vx = player.rigidBody.velocity.x;
        float vy = player.rigidBody.velocity.y;

        if (player.input.LeftHold())
        {
            vx = -3.0f;
            vy = 0.0f;
            player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", false);

            player.actionPoint.transform.localPosition = detectionPoints[1];
        }
        else if (player.input.RightHold())
        {
            vx = 3.0f;
            vy = 0.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", false);

            player.actionPoint.transform.localPosition = detectionPoints[1];
        }
        else if (player.input.UpHold())
        {
            vx = 0.0f;
            vy = 3.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", true);

            player.actionPoint.transform.localPosition = detectionPoints[2];
        }
        else if (player.input.DownHold())
        {
            vx = 0.0f;
            vy = -3.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", true);
            player.animator.SetBool("isFacingUp", false);

            player.actionPoint.transform.localPosition = detectionPoints[3];
        }
        else
        {
            vx = 0.0f;
            vy = 0.0f;
        }

        if (player.input.PickUp())
        {
            processAction();
        }

        if (player.input.Whistle())
        {
            player.transform.Find("Whistle").gameObject.SetActive(true);

            if (WhistleReset != null)
            {
                player.StopCoroutine(WhistleReset);
            }

            WhistleReset = player.StartCoroutine(ResetWhistle());

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemy = enemies[i].GetComponent<Enemy>();
                enemy.SetNewDestination(player.transform.position);
            }

        }

        if (player.input.DropSecurityGuard() && GameManager.instance != null && GameManager.instance.copCount > 0)
        {
            GameManager.instance.copCount -= 1;
            GameObject.Instantiate(PrefabManager.instance.STATIONARY_GUARD, player.gameObject.transform.position, Quaternion.identity);
        }

        player.rigidBody.velocity = new Vector2(
           vx,
           vy
        );

        player.animator.SetBool("isRunning", vx != 0 || vy != 0);
    }

    public void processAction()
    {
        if (player.GetTouchingObjects().Count > 0)
        {
            player.GetTouchingObjects().ForEach(delegate (GameObject touchingObject)
            {
                IPlayerActionable actionable = touchingObject.GetComponentInParent<IPlayerActionable>();

                if (actionable != null)
                {
                    actionable.PlayerAction(player);
                }
            });
        }
    }

    public void CheckCollider()
    {

    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "EnemyRadar")
        {
            GameObject.Destroy(player.gameObject);
            GameObject.Instantiate(PrefabManager.instance.SMOKE, player.transform.position, Quaternion.identity);

            if (GameManager.instance != null)
            {
                GameManager.instance.LoseLife();
            }
        }
    }

    private IEnumerator ResetWhistle()
    {
        yield return new WaitForSeconds(2.0f);
        player.transform.Find("Whistle").gameObject.SetActive(false);
    }
}
