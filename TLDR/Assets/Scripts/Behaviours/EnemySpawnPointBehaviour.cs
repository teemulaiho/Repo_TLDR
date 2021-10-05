using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointBehaviour : MonoBehaviour
{
    SpawnManager spawnManager;
    EnemyManager enemyManager;
    TimerManager timerManager;

    public List<EnemyBehaviour> spawnedEnemies;

    Color defaultColor;

    [SerializeField] LayerMask layerMask; // Use "Ground", when moving object raycast is from main camera to ground.

    [SerializeField] float spawnPointDT = 0f;
    //[SerializeField] float spawnPointTimer = 8f;

    [SerializeField] float deactivationDT = 0f;
    //[SerializeField] float deactivationTimer = 30f;

    bool beingPlaced = false;
    bool selected = false;
    GameObject selectedIndicator;

    float originalScaleX = 0f;



    public void Initialize(SpawnManager sm, EnemyManager em)
    {
        spawnManager = sm;
        enemyManager = em;

        selectedIndicator = transform.Find("SelectionIndicator").gameObject;
    }

    private void Awake()
    {
        defaultColor = this.GetComponent<MeshRenderer>().material.color;
        originalScaleX = transform.localScale.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerManager = spawnManager.GetTimerManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedEnemies != null && spawnedEnemies.Count > 0)
            deactivationDT += Time.deltaTime;
        spawnPointDT += Time.deltaTime;

        GetPlayerInput();
        
        if (deactivationDT >= /*deactivationTimer*/ timerManager.GetSpawnPointDeactivationTimer())
        {
            spawnPointDT = 0;

            float scaleTarget = 0f;
            float lerpVal = Mathf.Lerp(transform.localScale.x, scaleTarget, Time.deltaTime);
            transform.localScale = new Vector3(lerpVal, lerpVal, lerpVal);

            if (transform.localScale.x <= 0.01f)
                this.gameObject.SetActive(false);
            //enemyManager.DeactivateEnemies(spawnedEnemies);
        }

        if (timerManager.GetEnemySpawnRateTimer() == 0)
        {

        }
        else
        {
            if (spawnPointDT >= /*spawnPointTimer*/ timerManager.GetEnemySpawnRateTimer() * 0.5f)
            {
                var pingpong = Mathf.PingPong(Time.time, 1);
                var color = Color.Lerp(defaultColor, Color.red, pingpong);
                this.GetComponent<MeshRenderer>().material.color = color;

                float scaleMultiplier = 1.5f;
                float lerpScale = Mathf.Lerp(originalScaleX, scaleMultiplier, pingpong);

                Vector3 newScale = new Vector3(lerpScale, lerpScale, lerpScale);
                transform.localScale = newScale;
            }
            else
            {
                var color = Color.Lerp(this.GetComponent<MeshRenderer>().material.color, defaultColor, 1);
                this.GetComponent<MeshRenderer>().material.color = color;
            }

            if (spawnPointDT >= /*spawnPointTimer*/ timerManager.GetEnemySpawnRateTimer())
            {
                spawnPointDT = 0f;
                spawnedEnemies.Add(enemyManager.SpawnEnemy(this));
            }
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

    public void SelectObject(int mouseButton)
    {
        selected = true;
    }

    public void DeselectObject()
    {
        selected = false;
    }
}
