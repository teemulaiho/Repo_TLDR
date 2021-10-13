using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;

    private float currentHealth;

    public event Action<float> SetHealth;

    public float GetMaxHealth() { return maxHealth; }
    public float GetCurrentHealth() { return currentHealth; }

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
}
