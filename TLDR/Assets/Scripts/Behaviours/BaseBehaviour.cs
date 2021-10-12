using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    HealthbarBehaviour healthBar;
    int baseMaxHealth = 30;
    int baseHealth = 30;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthbarBehaviour>();

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(baseMaxHealth);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        UpdateHealthBar();

        if (baseHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.SetHealth(baseHealth);
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBehaviour e = other.GetComponent<EnemyBehaviour>();

            baseHealth -= e.GetEnemyDamage();
        }
    }
}
