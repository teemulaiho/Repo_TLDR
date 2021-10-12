using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] GameObject environmentParent;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject ground;

    [SerializeField] List<GameObject> rocks;
    [SerializeField] List<GameObject> trees;

    private void Awake()
    {
        treePrefab = Resources.Load<GameObject>("Prefabs/Environment/WarturtlesTreePlaceholder_Test");
        rockPrefab = Resources.Load<GameObject>("Prefabs/Environment/WarturtlesRockPlaceholder_Test");
    }

    // Start is called before the first frame update
    void Start()
    {
        rocks = new List<GameObject>();
        trees = new List<GameObject>();

        //GenerateEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateEnvironment()
    {
        if (ground != null)
        {
            int x = (int)ground.transform.localScale.x * 5;
            int z = (int)ground.transform.localScale.z * 5;

            for (int i = 0 - x; i < x; i++)
            {
                for (int j = 0 - z; j < z; j++)
                {
                    if (i % 64 + j % 77 == 0)
                    {
                        GameObject t = Instantiate(treePrefab);
                        t.transform.SetParent(environmentParent.transform);
                        t.transform.position = new Vector3(i, 0, j);
                        trees.Add(t);
                    }
                }
            }
        }
    }
}
