using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointBehaviour : MonoBehaviour
{
    SpawnManager spawnManager;
    EnemyManager enemyManager;

    [SerializeField] LayerMask layerMask; // Use "Ground", when moving object raycast is from main camera to ground.

    [SerializeField] float spawnPointDT = 0f;
    [SerializeField] float spawnPointTimer = 8f;

    [SerializeField] float deactivationDT;
    [SerializeField] float deactivationTimer = 30f;

    bool beingPlaced = false;
    bool selected = false;
    GameObject selectedIndicator;

    public void Initialize(SpawnManager sm, EnemyManager em)
    {
        spawnManager = sm;
        enemyManager = em;

        selectedIndicator = transform.Find("SelectionIndicator").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deactivationDT += Time.deltaTime;
        spawnPointDT += Time.deltaTime;

        GetPlayerInput();

        if (spawnPointDT >= spawnPointTimer)
        {
            enemyManager.SpawnEnemy(this);
            spawnPointDT = 0f;
        }


        if (deactivationDT >= deactivationTimer)
        {
            //this.gameObject.SetActive(false);
        }


        if (selected)
        {
            selectedIndicator.SetActive(selected);
        }
        else
            selectedIndicator.SetActive(false);

        if (!beingPlaced)
        {

        }
        else
        {
            WaitForPlacement();
        }
    }

    private void GetPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (selected)
                MoveObject(true);
        }
    }

    private void WaitForPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(this.gameObject);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            float x = raycastHit.point.x;
            float z = raycastHit.point.z;
            transform.position = new Vector3(x, transform.position.y, z);
        }

        if (Input.GetMouseButtonDown(1))
        {
            PlaceObject();
        }
    }

    public float GetTimer()
    {
        return spawnPointDT;
    }

    public void SelectObject(int mouseButton)
    {
        selected = true;
    }

    private void PlaceObject()
    {
        beingPlaced = false;
    }

    private void MoveObject(bool isMovable)
    {
        if (isMovable)
        {
            beingPlaced = true;
        }
    }
}
