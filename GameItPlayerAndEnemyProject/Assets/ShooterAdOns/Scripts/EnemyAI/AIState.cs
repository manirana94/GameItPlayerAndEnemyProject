using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    protected AIController enemyController;

    public AIState(AIController enemyController)
    {
        this.enemyController = enemyController;
    }

    public abstract System.Action Enter { get; }
    public abstract System.Action Execute { get; }
    public abstract System.Action Exit { get; }
    public abstract Type Transition();
}