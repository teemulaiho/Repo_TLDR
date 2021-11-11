using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridMaster : MonoBehaviour
{
    [SerializeField] GameObject[] columns;
    [SerializeField] private Hex[] hexes;

    [HideInInspector] public int gridWidth = 7;
    [HideInInspector] public int gridHeight = 8;

    private void Start()
    {
        hexes = GetComponentsInChildren<Hex>();
    }

    public void ShowGrid(bool state)
    {
        if (state)
        {
            foreach (Hex hex in hexes)
            {
                hex.HexChangeColor(1);
            }
        }
        else
        {
            foreach (Hex hex in hexes)
            {
                hex.HexChangeColor(0);
            }
        }
    }

    public Hex GetHexAt(int x, int y)
    {
        if (x < 0 || x > columns.Length - 1) { return null; }

        GameObject column = columns[x];

        if (column == null) { return null; }

        Transform columnTransform = column.transform;
        Transform hexTransform = columnTransform.Find($"Hex_{x}_{y}");

        if (hexTransform == null) { return null; }

        Hex hex = hexTransform.GetComponent<Hex>();

        return hex;
    } 
}
