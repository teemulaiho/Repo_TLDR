using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    ParticleSystem explosion;

    private void Awake()
    {
        explosion = GetComponent<ParticleSystem>();

        explosion.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(Vector3 position)
    {
        transform.position = position;

        if (!explosion.isPlaying)
        {
            explosion.Play();
        }
        else
        {
            explosion.Stop();
            explosion.Play();
        }
    }
}
