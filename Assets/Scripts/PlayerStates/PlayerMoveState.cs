using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMoveState : IState
{
    Player player;

    private Vector2[] detectionPoints = {
        new Vector2(-1.0f, 0.0f),
        new Vector2(1.0f, 0.0f),
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
            GameObject wall = player.GetTouchingObjects().Find(delegate (GameObject bk)
                {
                    return bk.name == "Wall";
                }
            );

            if (wall != null)
            {
                Vector3Int position = Vector3Int.FloorToInt(player.actionPoint.transform.position);
                position.z = 0;
            }
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
        }
    }
}