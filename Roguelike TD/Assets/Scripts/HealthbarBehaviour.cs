using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    private Health healthScript;

    /*
     * When adding a HealthBar to a GameObject, remember to add the following code to the GameObjects Awake -function:
     * 
     * healthBar = GetComponentInChildren<HealthbarBehaviour>();
     * if (healthBar != null)
     * {
     *      healthBar.SetMaxHealth(baseMaxHealth);
     * }  
     * 
     * OR
     * 
     * Add the GameObject or Health Reference to the HealthBar instead.
     * 
     * GameObject parent = GetComponentInParent<GameObject>();
     * 
     *         health = GetComponentInParent<Health>();
     *         if (health != null)
     *         {
     *              SetMaxHealth(health.GetMaxHealth());
     *         }
    */

    private void Start()
    {
        healthScript = GetComponentInParent<Health>();

        if (healthScript != null)
        {
            SetMaxHealth(healthScript.GetMaxHealth());
            healthScript.SetHealth += SetHealth;
        }
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
