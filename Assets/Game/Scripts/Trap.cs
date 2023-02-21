using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private float falldelay = 1f;
    private float destroydelay = 2f;
    private Character character;
    [SerializeField] private GameObject trapactive;

    [SerializeField] private Rigidbody2D rb;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (trapactive.CompareTag("Player"))
            {
                collision.GetComponent<Character>().OnHit(40f);
            }
            
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(falldelay);
        rb.bodyType = RigidbodyType2D.Dynamic;  
        //character.OnHit(20f);
        Destroy(gameObject,destroydelay);
    }
}
