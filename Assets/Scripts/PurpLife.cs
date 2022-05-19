using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpLife : MonoBehaviour
{


    private Animator anim;


    private void Start()
    {

        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("die"))
        {
            Die();

        }

    }

    private void Die()
    {
        anim.SetTrigger("die");
       // transform.position = new Vector3(-7.5f, -3, -10);
    }
}