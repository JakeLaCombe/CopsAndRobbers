using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PatrolTypes patrolType;
    public StateMachine stateMachine;
    public Vector3 patrolDestination;
    public EnemyMove enemyMoveState;
    public Vector3 startingDirection = new Vector3(1.0f, 1.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine();
        enemyMoveState = new EnemyMove(this, patrolType, patrolDestination, startingDirection);
        stateMachine.ChangeState(enemyMoveState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Execute();
    }
}


public enum PatrolTypes
{
    STANDING,
    MOVING,
}