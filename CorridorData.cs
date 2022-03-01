using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorData
{
    public HashSet<Vector2Int> Path { get; private set; }
    public Vector2Int FromTile { get; private set; }
    public Vector2Int ToTile { get; private set; }

    public CorridorData(Vector2Int fromTile, Vector2Int toTile, HashSet<Vector2Int> path)
    {
        FromTile = fromTile;
        ToTile = toTile;
        Path = path;
    }
}
