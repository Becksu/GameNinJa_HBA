using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    [SerializeField] private int addHeath;
    [SerializeField] private float duration;
    
    private Player player;
    private float timer ;
    private bool isAddHeath;


    public void OnInit(Player player)
    {
       
        this.player = player;

        
    }

    private void Update()
    {
        if (isAddHeath)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                player.HealthBuff(addHeath);
                timer = duration;
            }
        }
    }
    ////////////////////////////
    public void SetAddHealth(bool isAddHeath)
    {
        this.isAddHeath = isAddHeath;
        if (isAddHeath)
        {
            timer = duration;
        }
    }
    //private void Start()
    //{
    //    timer = duration;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Player player = GetComponent<Player>();
    //        player.HealthBuff(addHeath);
    //    }
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        timer -= Time.deltaTime;
    //        if (timer <= 0)
    //        {
    //            Player player = GetComponent<Player>();
    //            player.HealthBuff(addHeath);
    //            if (player.HP == 100)
    //            {
    //                addHeath = 0;
    //            }
    //        }
           
    //    }
    //}



}
