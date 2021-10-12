using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    /*
     * When adding a HealthBar to a GameObject, remember to add the following code to the GameObjects Awake -function:
     * 
     * healthBar = GetComponentInChildren<HealthbarBehaviour>();
     * if (healthBar != null)
     * {
     *      healthBar.SetMaxHealth(baseMaxHealth);
     * }   
    */

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
