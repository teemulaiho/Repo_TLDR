using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionBehaviour : MonoBehaviour
{
    EnemyBehaviour enemy;
    [SerializeField] List<Sprite> reactions;

    Image reactionImage;

    public void Initialize(EnemyBehaviour e)
    {
        enemy = e;
    }

    private void Awake()
    {
        reactions.Add(Resources.Load<Sprite>("Sprites/QuestionMark"));
        reactions.Add(Resources.Load<Sprite>("Sprites/ExclamationMark"));

        reactionImage = transform.Find("ReactionImage").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetReactionSprite(int i)
    {
        if (reactions.Count >= i)
            reactionImage.sprite = reactions[i];
    }
}
