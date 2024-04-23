using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : AIState
{
    public PatrolState(AIController enemyController) : base(enemyController)
    {
    }

    public override System.Action Enter => () =>
    {
        enemyController.anim.SetBool("isPatrolling", true);
        enemyController.anim.SetBool("isIdle", false);
        enemyController.anim.SetBool("isAttacking", false);
    };

    public override System.Action Execute => () =>
    {
        enemyController.Patrol();
    };

    public override System.Action Exit => () =>
    {
        enemyController.anim.SetBool("isPatrolling", false);
        enemyController.anim.SetBool("isIdle", false);
        enemyController.anim.SetBool("isAttacking", false);
    };

    public override Type Transition()
    {
        if (enemyController.IsPlayerInRange())
        {
            return typeof(AttackState);
        }
        else
        {
            return typeof(PatrolState);
        }
    }
}