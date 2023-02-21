using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public GameObject skill;
    public Rigidbody2D rb;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnInit()
    {
        rb.velocity = transform.right * 5;
        Invoke(nameof(Ondespawn), 2f);
    }
    void Ondespawn()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Character>().OnHit(50f);
            Ondespawn();
        }
    }
}
