using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBehaviour : MonoBehaviour
{
    List<GameObject> bullets;

    List<GameObject> targetPos;
    List<Vector3> targets;

    public float bulletSpeed = 5f;
    public float radius = 10f;
    public float degrees = 36f;
    public float YDegrees = 0f;

    public float currentYRotEuler = 0f;

    public float deleteTimer = 0f;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        targets = new List<Vector3>();
        targetPos = new List<GameObject>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullets();
        }

        if (bullets != null &&
            bullets.Count > 0)
        {
            deleteTimer += Time.deltaTime;

            for (int i = 0; i < 5; i++)
            {
                bullets[i].transform.position = Vector3.MoveTowards(bullets[i].transform.position, targets[i], bulletSpeed * Time.deltaTime);

                Vector3 targetDir = targets[i] - bullets[i].transform.position;
                float angleBetween = Vector3.Angle(transform.forward, targetDir);

                Debug.Log("i: " + i + " angleBetwen: " + angleBetween);
            }

            if (deleteTimer > 10f)
            {
                foreach (GameObject b in bullets)
                {
                    Destroy(b);
                }

                bullets.Clear();

                foreach(GameObject tp in targetPos)
                {
                    Destroy(tp);
                }

                targetPos.Clear();

                targets.Clear();

                deleteTimer = 0f;
            }
        }
    }

    private void SpawnBullets()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject b = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            b.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            bullets.Add(b);

            currentYRotEuler = transform.rotation.eulerAngles.y;            

            //float radians = degrees * Mathf.Deg2Rad;
            //float radians = degrees * Mathf.Deg2Rad;

            YDegrees = transform.rotation.eulerAngles.y;
            YDegrees += 16f * i;

            float radians = YDegrees * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians);
            float z = -Mathf.Sin(radians);

            Vector3 t = new Vector3(x, 1f, z);
            t = t * radius;
            t += transform.position;

            targets.Add(t);

            GameObject tp = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
            tp.transform.position = t;
            targetPos.Add(tp);

            //degrees += 16f;
        }
    }
}
