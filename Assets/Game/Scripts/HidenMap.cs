using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidenMap : MonoBehaviour
{
    [SerializeField] private GameObject _maphidenPoint;
    [SerializeField] private GameObject _maphidenExit;
    [SerializeField] private GameObject playermove;
    private Player condition;

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Vector3 move = _maphidenExit.transform.position;
            playermove.transform.position = move;
        }
    }
}
