using System;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public Column[] Columns;
    public SpriteRenderer[] Renderers;
    
}

[Serializable]
public class Column
{
    public Transform ColumnTransform;
    public SpriteRenderer[] ColumnData;
}
