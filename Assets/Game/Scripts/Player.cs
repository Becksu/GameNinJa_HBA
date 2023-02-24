using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
   
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    [SerializeField] private float jumpForce;
    [SerializeField] private Kunai kunaiFrefab;
    [SerializeField] private Skill skillfrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    //[SerializeField] private Character hpheath;
    [SerializeField] private HealthBuff healthBuff;

    private int combocount;
    public int ComboCount
    {
        get
        {
            return combocount;
        }
        set 
        { 
            combocount = value;
        }

    }
    private float timerattack = 0.5f;
    

    private bool isGrounded = true;
    private bool isJumping = false;
    public bool isAttack = false;
    private bool isDeath = false;

   
    private float horizontal;
    private float addHp;

    private DateTime timeCombo;

    private int coin = 0;
    private Vector3 savePoint;
    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGrounded();
        //horizontal = Input.GetAxisRaw("Horizontal");
        //verticle = Input.GetAxisRaw("Verticle");
        if (isDeathp)
        {
            return;
        }
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            if (isAttack)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangAnim("run");
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Attack();
                
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                Throw();               
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Teleport();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Skill();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                ComboAttack();
            }
           
        }
        
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangAnim("fall");
            isJumping = false;
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0:180,0));
        }
        else if(isGrounded && isAttack == false)
        {
            ChangAnim("idle");
            rb.velocity = Vector2.zero;
        }
        //healthBuff.OnExcute();
    }

    public override void OnInit()
    {
        base.OnInit();
        isAttack = false;
        transform.position = savePoint;
        ChangAnim("idle");
        DeActiveAttack();
        SavePoint();
        UIManager.instance.SetCoint(coin);
        healthBuff.OnInit(this);
        timeCombo = DateTime.Now;

    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    public override void OnDeath()
    {
        base.OnDeath();
       
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        //if(hit.collider != null)
        //{
        //    return true;

        //}
        //else
        //{
        //    return false;
        //}
        return hit.collider != null;
    }
    public void Attack()
    {
        isAttack = true;
        ChangAnim("attack");
        Invoke(nameof(ResetAction), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
        double detaltime = (DateTime.Now - timeCombo).TotalMilliseconds;
        if(detaltime < 500)
        {
            ComboCount++;           
        }
        else
        {
            ComboCount = 1;
        }
        timeCombo = DateTime.Now;
        
    }
    public void ComboAttack()
    {
        if (timerattack > 0 && combocount == 2)
        {
            Debug.Log("asbdk");
        }
    }

    public void HealthBuff(float heath)
    {
        if (HP >= maxHp)
        {
            return;
        }
        HP+=heath;
        Debug.Log("àdgdgggsd");
        isDeath = false;
    }
    public void Throw()
    {
        isAttack = true;
        ChangAnim("throw");
        Invoke(nameof(ResetAction),0.5f);
        Instantiate(kunaiFrefab,throwPoint.position,throwPoint.rotation);
    }
    public void Skill()
    {
        Instantiate(skillfrefab, transform.position, Quaternion.identity);
    }
    public void Jump()
    {

        isJumping = true;
        rb.AddForce(jumpForce * Vector2.up);
        ChangAnim("jump");
    }
 
    
    private void ResetAction()
    {
        //ChangAnim("idle");    
        isAttack = false;
    }
    private void Teleport()
    {            
       
            transform.position += transform.right * 3;         
    }
   
   
    internal void SavePoint()
    {
        savePoint = transform.position;
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.tag == "Coin")
        {
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.instance.SetCoint(coin);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Heathbuff"))
        {
            Debug.Log("huynh");
            healthBuff.SetAddHealth(true);
        }
        else if (collision.tag == "Death")
        {
           
            ChangAnim("die");
            Invoke(nameof(OnInit), 1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Heathbuff"))
        {
            Debug.Log("huynh");
            healthBuff.SetAddHealth(false);
        }
    }
    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }

   

}
