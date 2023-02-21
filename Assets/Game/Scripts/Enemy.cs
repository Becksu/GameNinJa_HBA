using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;

    private bool isRight = true;
    private Character target;
    public Character Target => target;

    private IState currenState;
    private void Update()
    {
        if(currenState != null&&!isDeathp)
        {
            currenState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangState(new IdleState());
        DeActiveAttack();
    }
    public override void OnDeath()
    {
        ChangState(null);

        base.OnDeath();
    }
    public void ChangState(IState newState)
    {
        if(currenState!= null)
        {
            currenState.OnExit(this);
        }
        
        currenState = newState;
        if(currenState!= null)
        {
            currenState.OnEnter(this);
        }
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(heathBar.gameObject);
        Destroy(gameObject);
    }
    internal void SetTarget(Character character)
    {
        this.target = character;
        if (IsTargetRange())
        {
            ChangState(new AtackState());
        }
        else
        {

        }
        if(Target!= null)
        {
            ChangState(new PatrolState());
        }
        else
        {
            ChangState(new IdleState());
        }
    }
    public void Moving()
    {
        ChangAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }
    public void StopMoving()
    {
        ChangAnim("idle");
       
        rb.velocity = Vector2.zero;
    }
    public void Atack()
    {
        ChangAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }
    public bool IsTargetRange()
    {
        if(target!= null&& Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EmemyWall")
        {
            ChangDirection(!isRight);
        }
    }

    public void ChangDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
}
