using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    public event Action<float> SetHealth;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SetHealthEvent();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetHealthEvent()
    {
        if (SetHealth != null)
            SetHealth(currentHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
