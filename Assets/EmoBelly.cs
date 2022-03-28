using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoBelly : BellyEnemy
{
    private float attackSpeed = 1f;
    private bool attacking = false;
    private float attackDamage = 3f;
    private bool bleeding = false;
    private int bleedingTicks = 0;
    private float bleedingTickTimer = 0.5f;
    private float bleedingDamage = 0.5f;

    protected override void updateAttack()
    {
        nav.destination = playerObject.transform.position;

        if(nav.velocity == new Vector3(0,0,0) && !attacking && distance <= 1.5f)
        {
            StartCoroutine(Knife());
            attacking = true;
        }

        if(bleedingTicks > 0 && !bleeding)
        {
            StartCoroutine(BleedingEffect());
        }

    }

    IEnumerator Knife()
    {
        yield return new WaitForSeconds(attackSpeed);


        if(distance <= 1.5)
        {
            playerObject.GetComponent<HealthScript>().ReduceHealth(attackDamage);
            bleedingTicks = 6;
        }
        
        attacking = false;
    }


    IEnumerator BleedingEffect()
    {
        bleedingTicks -= 1;
        bleeding = true;

        yield return new WaitForSeconds(bleedingTickTimer);

        playerObject.GetComponent<HealthScript>().ReduceHealth(bleedingDamage);

        bleeding = false;
    }





}
