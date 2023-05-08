using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContactTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
