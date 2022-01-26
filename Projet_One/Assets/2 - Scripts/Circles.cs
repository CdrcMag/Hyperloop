using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Circles : MonoBehaviour
{
    public UnityEvent evenement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            evenement?.Invoke();
        }
    }
    

}
