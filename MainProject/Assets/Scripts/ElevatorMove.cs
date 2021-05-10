using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    public Animator animator;
    public GameObject instructions;

    public GameObject player;
    

    void Start()
    {
        player = Player.instance.gameObject;
    }    

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = transform;
        }


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
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }

        if (other.CompareTag("Player"))
        {
            instructions.SetActive(false);
        }       

    }
}
