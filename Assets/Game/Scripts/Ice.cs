using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] private GameObject hitVFX;

    private void Start()
    {
        OnInit();
    }
    private void OnInit()
    {
        rb.velocity = new Vector2(-2f, -speed);
        CancelInvoke();
        Invoke(nameof(OnDespawn), 3f);
    }

    private void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character>().OnHit(10f);
            //Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
    }
}