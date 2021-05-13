using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Ammo")]
    public int clipSize = 6, curClip = 6;
    public int maxAmmo = 18, ammo = 18;

    [Header("Stats")]
    public int DMG = 1;
    public float ROF = 0.5f;
    public int range = 30;
    public float spreadAt100m;

    float nextShot = 0;
    public Animator animator;

    [Header("Reloading")]
    public float reloadTime = 1.5f;
    public bool isReloading;

    [Header("Raycasting")]
    public Transform spawnPoint;
    public LayerMask hitMask = -1;

    [Header("Effects")]
    public GameObject muzzle;
    public GameObject impactEffect;


    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    public virtual void TriggerDown()
    {
        if (isReloading || Time.time < nextShot)
            return;
        
        
        if (curClip > 0)
        {
            curClip--;
            nextShot = Time.time + ROF;
            Shoot();
        }
        else
            Reload();
    }

    public virtual void TriggerHold()
    {
        Debug.Log("Trigger Hold");
        TriggerDown();
    }

    public virtual void TriggerUp()
    {
        Debug.Log("Trigger Up");
    }

    public virtual void Shoot()
    {
        GameObject currentMuzzle = Instantiate(muzzle, spawnPoint.position, spawnPoint.rotation);
        currentMuzzle.transform.parent = spawnPoint;

        //Define a ray
        RaycastHit hit;
        Ray gunRay = new Ray(spawnPoint.position, spawnPoint.forward);


        //Calculate Spread
        Vector3 spreadDir = (Random.insideUnitCircle * spreadAt100m) / 100;
        spreadDir = spawnPoint.TransformDirection(spreadDir);
        gunRay.direction += spreadDir;

        if (Physics.Raycast(gunRay, out hit, range, hitMask))
        {
            Debug.DrawLine(gunRay.origin, hit.point, Color.green, 0.125f);
        }
        else
        {
            Debug.DrawRay(gunRay.origin, gunRay.direction * range, Color.red, 0.125f);
        }
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(DMG);
            }
        }
        GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGameObject, .5f);
    }
    

    public virtual void Reload()
    {
        if (isReloading || ammo == 0 || curClip == clipSize)
            return;

        StartCoroutine(ReloadSequence());
    }

    protected virtual IEnumerator ReloadSequence()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        curClip = clipSize;

        isReloading = false;
    }
}
