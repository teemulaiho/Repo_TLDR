using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionBehaviour : MonoBehaviour
{
    EnemyBehaviour enemy;
    [SerializeField] List<Sprite> reactions;

    public void Initialize(EnemyBehaviour e)
    {
        enemy = e;
    }

    private void Awake()
    {
        reactions.Add(Resources.Load<Sprite>("Sprites/QuestionMark"));
        reactions.Add(Resources.Load<Sprite>("Sprites/ExclamationMark"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
