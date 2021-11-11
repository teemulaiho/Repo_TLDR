using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    public Hex[] myNeighbors = new Hex[6];

    [HideInInspector] public HexGridMaster hexGridMaster;

    [HideInInspector] public int Q;   // "Column"
    [HideInInspector] public int R;   // "Row"
    [HideInInspector] public int S;

    [HideInInspector] public int gCost;
    [HideInInspector] public int hCost;
    [HideInInspector] public int fCost;

    //public Unit unitOnHex = null;
    //public Unit incomingUnit = null;
    public Hex cameFromHex = null;

    [Header("Hex Visuals")]
    [SerializeField] private float fadeSpeed = 0.5f;
    [SerializeField] private float colorFadeSpeed = 12.5f;
    [SerializeField] private float secondColorFadeSpeed = 10f;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color hoveredColor = Color.black;
    [SerializeField] private Color hoveredSecondColor = Color.black;

    [Space, SerializeField] private GameObject unitPlacedVFX = null;
    [SerializeField] private AudioClip unitPlacedSFX = null;

    private SpriteRenderer hexSprite = null;
    private Coroutine fadeCoroutine;

    //public Unit GetUnitOnHex() { return unitOnHex; }

    private void Awake()
    {
        hexSprite = transform.Find("Hex_Sprite").GetComponent<SpriteRenderer>();
        hexGridMaster = FindObjectOfType<HexGridMaster>();

        Q = this.name[4] - 48;
        R = this.name[6] - 48;
        S = -(Q + R);

        //GetMyNeighbors();
    }

    private void Start()
    {
        HexChangeColor(0); 
    }

    private void GetMyNeighbors()
    {
        myNeighbors = new Hex[6];
        myNeighbors[1] = hexGridMaster.GetHexAt(Q + 1, R);
        myNeighbors[4] = hexGridMaster.GetHexAt(Q - 1, R);

        if (R % 2 == 0)
        {
            myNeighbors[0] = hexGridMaster.GetHexAt(Q, R + 1);
            myNeighbors[2] = hexGridMaster.GetHexAt(Q, R - 1);
            myNeighbors[3] = hexGridMaster.GetHexAt(Q - 1, R - 1);
            myNeighbors[5] = hexGridMaster.GetHexAt(Q - 1, R + 1);
        }
        else
        {
            myNeighbors[0] = hexGridMaster.GetHexAt(Q + 1, R + 1);
            myNeighbors[2] = hexGridMaster.GetHexAt(Q + 1, R - 1);
            myNeighbors[3] = hexGridMaster.GetHexAt(Q, R - 1);
            myNeighbors[5] = hexGridMaster.GetHexAt(Q, R + 1);
        } 
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    /*public void UnitRemoved()
    {
        if (unitOnHex == incomingUnit)
            incomingUnit = null;

        unitOnHex = null;
    }

    public void UnitEntered(Unit newUnit)   // During combat
    {
        unitOnHex = newUnit;
    }

    public void UnitPlaced(Unit newUnit)   // Before combat
    {
        unitOnHex = newUnit;
        unitOnHex.transform.position = transform.position;

        var effect = Instantiate(unitPlacedVFX, transform.position, Quaternion.identity);
        Destroy(effect, 0.75f);

        AudioSource.PlayClipAtPoint(unitPlacedSFX, transform.position, 1.0f);
    }

    public void UnitsSwitchSpots(Unit unit)
    {
        var selectedUnit = unit;
        var selectedUnitsHex = selectedUnit.CurrentHex;
        var originalUnit = unitOnHex;
        var originalHex = this;

        selectedUnit.SetHex(originalHex);   // Set dragged unit to this hex

        originalUnit.SetHex(selectedUnitsHex);  // Set current unit to the dragged one's hex
    }*/

    public void HexChangeColor(int state)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (state == 0) // Hide
            fadeCoroutine = StartCoroutine(FadeOut());
        else if (state == 1)    // Active but not hovered
            fadeCoroutine = StartCoroutine(FadeIn(defaultColor));
        else if (state == 2)    // Hovered
            fadeCoroutine = StartCoroutine(FadeToColor(hoveredColor));
        else if (state == 3)    // No longer hovered
            fadeCoroutine = StartCoroutine(FadeToColor(defaultColor));
    }

    private IEnumerator FadeIn(Color targetColor)
    {
        hexSprite.color = new Color(targetColor.r, targetColor.g, targetColor.b, hexSprite.color.a);

        Color tmpColor = hexSprite.color;
        float targetAlpha = targetColor.a;

        while (hexSprite.color.a < targetAlpha)
        {
            tmpColor.a += Time.deltaTime / fadeSpeed;
            hexSprite.color = tmpColor;

            if (tmpColor.a >= targetAlpha)
                tmpColor.a = targetAlpha;

            yield return null;
        }

        hexSprite.color = tmpColor;
    }

    private IEnumerator FadeToColor(Color targetColor)
    {
        Color tmpColor = hexSprite.color;

        while (hexSprite.color != targetColor)
        {
            float t = Time.deltaTime * colorFadeSpeed;

            tmpColor = Color.Lerp(tmpColor, targetColor, t);
            tmpColor.a = Mathf.Lerp(tmpColor.a, targetColor.a, t);

            hexSprite.color = tmpColor;

            yield return null;
        }

        if (targetColor != hoveredColor) yield break;   // If still hovered, continue

        while (hexSprite.color != hoveredSecondColor)
        {
            float t = Time.deltaTime * secondColorFadeSpeed;

            tmpColor = Color.Lerp(tmpColor, hoveredSecondColor, t);
            tmpColor.a = Mathf.Lerp(tmpColor.a, hoveredSecondColor.a, t);

            hexSprite.color = tmpColor;

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        Color tmpColor = hexSprite.color;

        while (hexSprite.color.a > 0)
        {
            tmpColor.a -= Time.deltaTime / fadeSpeed;
            hexSprite.color = tmpColor;

            if (tmpColor.a <= 0)
                tmpColor.a = 0;

            yield return null;
        }

        hexSprite.color = tmpColor;
    }
}
