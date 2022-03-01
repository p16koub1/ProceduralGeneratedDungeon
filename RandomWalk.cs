using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomWalk
{
    public static HashSet<Vector2Int> RunSimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);

        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);

            previousPosition = newPosition;
        }

        return path;
    }

    public static HashSet<Vector2Int> RunSimpleRandomWalkInBounds(Vector2Int startPosition, int walkLength, BoundsInt bounds)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);

        var previousPosition = startPosition;

        for (int steps = 0; steps < walkLength; steps++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            if (!bounds.Contains((Vector3Int)newPosition))
                break;

            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }
}
