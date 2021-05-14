using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Transform WeaponHolder;
    public GameObject instructions;

    void OnTriggerStay()
    {
        instructions.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(instructions.gameObject);
            this.transform.position = WeaponHolder.position;
            this.transform.parent = GameObject.Find("Weapon Holder").transform;

        }

    }

    void OnTriggerExit()
    {
        instructions.SetActive(false);
    }
}
