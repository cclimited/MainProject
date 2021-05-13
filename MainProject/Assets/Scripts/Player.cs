using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public CharacterController controller;

    public WeaponSwitch weaponSwitcher;
    
    public float health;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;

    public GameObject Light;
    public bool LightActive;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Light.SetActive(true);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            weaponSwitcher.weapons[weaponSwitcher.selectedWeapon].TriggerDown();
        else if (Input.GetKey(KeyCode.Mouse0))
            weaponSwitcher.weapons[weaponSwitcher.selectedWeapon].TriggerHold();
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            weaponSwitcher.weapons[weaponSwitcher.selectedWeapon].TriggerUp();

        if (Input.GetKeyDown(KeyCode.R))
            weaponSwitcher.weapons[weaponSwitcher.selectedWeapon].Reload();

        //FlashLight
        if (Input.GetKeyDown(KeyCode.F))
        {
            LightActive = !LightActive;

            if (LightActive)
            {
                FlashLightActive();
            }

            if (!LightActive)
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
