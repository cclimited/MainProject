using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject Light;
    public bool LightActive;

    void Start()
    {
        Light.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LightActive = !LightActive;

            if(LightActive)
            {
                FlashLightActive();
            }

            if(!LightActive)
            {
                FlashLightInactive();
            }
        }
       
    }

    void FlashLightActive()
    {
        Light.SetActive(true);
    }

    void FlashLightInactive()
    {
        Light.SetActive(false);
    }
}
