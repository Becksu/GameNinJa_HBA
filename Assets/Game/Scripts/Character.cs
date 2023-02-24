using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HeathBar heathBar;
    [SerializeField] protected TextCombat CombatTextPrefab;
   
    [SerializeField] private float hp;
    protected float maxHp;
    public float HP { get { return hp; } set { hp = value; } }
    private string currenAnim;
    public bool isDeathp => hp <= 0;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        maxHp = 100;
        hp = maxHp;
        heathBar.OnInit(maxHp,transform);
        
        
    }
    public virtual void OnDeath()
    {
        ChangAnim("die");
        Invoke(nameof(OnDespawn),2f);
    }
    public virtual void OnDespawn()
    {

    }
    protected void ChangAnim(string animName)
    {
        if (currenAnim != animName)
        {
            anim.ResetTrigger(animName);
            currenAnim = animName;
            anim.SetTrigger(currenAnim);
        }
    }
    public void OnHit(float damage)
    {
        Debug.Log("hit");
        if (!isDeathp)
        {
            hp = hp-damage; 
            if(isDeathp)
            {
                hp = 0;
                OnDeath();
            }
            heathBar.SetNewHp(hp);
            Instantiate(CombatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }


}
