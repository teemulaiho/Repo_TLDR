using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private float tPlacementRange;
    [SerializeField] private float spawnerActivationRange;

    private void OnDrawGizmosSelected() // Turret range indicator (only inspector)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tPlacementRange);
    }
}
