using UnityEngine;

abstract public class HealthScript : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float currentHealth = 100f;

    private void Update()
    {
        CheckIfDead();
    }


    public void ReduceHealth(float amount)
    {
        currentHealth -= amount;
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
    }

    public void SetHealth(float amount)
    {
        currentHealth = amount;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void SetMaxHealth(float amount)
    {
        maxHealth = amount;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    protected abstract void OnDeath();

    protected void CheckIfDead()
    {
        if(currentHealth <= 0)
        {
            OnDeath();
        }
    }

}
