using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    public HashSet<Vector2Int> FloorPositions { get; private set; }
    public Vector2Int RoomCenter { get; private set; }

    public RoomData(HashSet<Vector2Int> floorPositions, Vector2Int roomCenter)
    {
        FloorPositions = floorPositions;
        RoomCenter = roomCenter;
    }
}
