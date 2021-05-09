using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    public Animator animator;
    public GameObject instructions;


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            instructions.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
                animator.SetTrigger("MoveForward");

            if (Input.GetKeyDown(KeyCode.B))
                animator.SetTrigger("MoveBackward");


        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instructions.SetActive(false);
        }

    }
}
