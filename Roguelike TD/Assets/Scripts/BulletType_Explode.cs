using UnityEngine;

public class BulletType_Explode : MonoBehaviour
{
    [SerializeField] private Bullet bulletScript;
    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] SphereCollider bulletExplosionCollider;

    private void Awake()
    {
        if (bulletScript == null)
            bulletScript = GetComponent<Bullet>();

        if (explosionPrefab == null)
            explosionPrefab = Resources.Load<ParticleSystem>("Prefabs/Explosion");

        if (bulletExplosionCollider == null)
            bulletExplosionCollider = GetComponent<SphereCollider>();

        if (bulletExplosionCollider != null)
        {
            bulletExplosionCollider.enabled = false;
        }
    }

    private void Start()
    {
        if (bulletScript != null)
        {
            bulletScript.HitNow += HitNow;
        }
    }

    private void HitNow(Transform transform)
    {
        ParticleSystem explosion = Instantiate(explosionPrefab);
        explosion.tag = "Bullet";
        //explosion.GetComponent<SphereCollider>().enabled = true;
        explosion.transform.position = transform.position;

        explosion.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy collider.");
            bulletScript.HitDestination();
            //HitNow(collision.gameObject.transform);
        }
    }
}
