using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState
{
    private float attackTimer = 0f;
    private float attackDuration = 5f; 
    public AttackState(AIController enemyController) : base(enemyController)
    {
    }

    public override System.Action Enter => () =>
    {
        enemyController.anim.SetBool("isAttacking", true);
        enemyController.anim.SetBool("isIdle", false);
        enemyController.anim.SetBool("isPatrolling", false);
        attackTimer = 0f;
    };

    public override System.Action Execute => () =>
    {
        enemyController.Attack();
        attackTimer += Time.deltaTime;
    };

    public override System.Action Exit => () =>
    {
        enemyController.anim.SetBool("isAttacking", false);
        enemyController.anim.SetBool("isIdle", false);
        enemyController.anim.SetBool("isPatrolling", false);
    };

    public override Type Transition()
    {
        if (attackTimer >= attackDuration)
        {
            return typeof(PatrolState);
        }
        else if (!enemyController.IsPlayerInRange())
        {
            return typeof(PatrolState);
        }
        else
        {
            return typeof(AttackState);
        }
    }
}