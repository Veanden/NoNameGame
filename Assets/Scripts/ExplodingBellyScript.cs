using System.Collections;
using UnityEngine;


public class ExplodingBellyScript : BellyEnemy
{
    [SerializeField] private float explosionDistance = 4f;
    [SerializeField] private float explosionRadius = 6f;
    [SerializeField] private float explosionTime = 2f;
    [SerializeField] private float explosionDamage = 100f;
    private bool exploded = false;

    protected override void setSpeed()
    {
        nav.speed = Random.Range(5f, 15f);
    }

    protected override void updateAttack()
    {
        if (!exploded)
            nav.destination = playerObject.transform.position;

        

        if (!exploded && distance <= explosionDistance)
        {
            StartCoroutine(Explode());
            nav.enabled = false;
            exploded = true;
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionTime);

        distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (distance < explosionRadius)
        {
            float damageMultiplier = 1 / (explosionRadius / (explosionRadius - distance));

            playerObject.GetComponent<HealthScript>().ReduceHealth(explosionDamage * damageMultiplier);
        }

        Destroy(transform.gameObject);
    }
}
