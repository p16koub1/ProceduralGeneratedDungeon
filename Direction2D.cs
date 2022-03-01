using System.Collections.Generic;
using UnityEngine;

public static class Direction2D
{
    // Directions List
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), // Up
        new Vector2Int(1, 0), // Right
        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, 0) // Left
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>()
    {
        new Vector2Int(1, 1), // Up - Right
        new Vector2Int(1, -1), // Up - Left
        new Vector2Int(-1, 1), // Down - Right
        new Vector2Int(-1, -1) // Down - Left
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }

    public static Vector2Int GetRandomDiagonalDirection()
    {
        return diagonalDirectionsList[Random.Range(0, diagonalDirectionsList.Count)];
    }

    public static Vector2Int GetRandomDirection()
    {
        var allDirectionsList = new List<Vector2Int>(diagonalDirectionsList.Count + cardinalDirectionsList.Count);
        allDirectionsList.AddRange(diagonalDirectionsList);
        allDirectionsList.AddRange(cardinalDirectionsList);

        return allDirectionsList[Random.Range(0, allDirectionsList.Count)];
    }
}
