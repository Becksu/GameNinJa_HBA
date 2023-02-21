using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackState : IState
{
    float timer;
    public void OnEnter(Enemy enemy)
    {
        if(enemy.Target!= null)
        {
            enemy.ChangDirection(enemy.Target.transform.position.x > enemy.transform.position.x);         
            enemy.Atack();
            enemy.StopMoving();
        }
        timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            enemy.ChangState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }

}
