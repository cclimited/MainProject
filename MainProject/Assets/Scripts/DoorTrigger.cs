using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {       
        if (other.CompareTag("Player"))
        {
            animator.SetBool("open", true);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("open", false);
        }

    }
}
