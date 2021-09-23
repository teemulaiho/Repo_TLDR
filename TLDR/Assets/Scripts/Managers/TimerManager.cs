using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    GameManager gameManager;

    public List<float> timers;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timers.Clear();

        timers.AddRange(gameManager.GetTimers());
    }

    public void AddTimer(ref float timer)
    {
        timers.Add(timer);
    }
}
