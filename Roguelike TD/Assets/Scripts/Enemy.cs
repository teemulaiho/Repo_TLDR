using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private NavMeshAgent enemyAgent;
    [SerializeField] private Health healthScript;

    private float enemyDamage;

    private void Awake()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Start()
    {
        enemyAgent.SetDestination(new Vector3(0, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            Health baseHealth = other.transform.GetComponent<Health>();

            if (baseHealth != null)
            {
                baseHealth.TakeDamage(enemyDamage);
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.transform.GetComponent<Bullet>();

            if (bullet != null)
            {
                float damage = bullet.GetDamage();

                healthScript.TakeDamage(damage);
            }

            Destroy(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        waveManager.RemoveEnemyFromList(this);
    }
}
