using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public IdleState(AIController enemyController) : base(enemyController)
    {
    }

    public override System.Action Enter => () =>
    {
        enemyController.anim.SetBool("isIdle", true);
        enemyController.anim.SetBool("isPatrolling", false);
        enemyController.anim.SetBool("isAttacking", false);

    };

    public override System.Action Execute => () =>
    {
        // Idle behavior here
    };

    public override System.Action Exit => () =>
    {
         enemyController.anim.SetBool("isIdle", false);
        enemyController.anim.SetBool("isPatrolling", false);
        enemyController.anim.SetBool("isAttacking", false);

    };

    public override Type Transition()
    {
        // Check for transitions to other states
        if (enemyController.IsPlayerInRange())
        {
            return typeof(AttackState);
        }
        else if (enemyController.ShouldPatrol())
        {
            return typeof(PatrolState);
        }
        else
        {
            return typeof(IdleState);
        }
    }
}