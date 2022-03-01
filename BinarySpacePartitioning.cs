using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BinarySpacePartitioning
{
    public static List<BoundsInt> RunBinarySpacePartitioning(BoundsInt spaceToSplit, int minimumWidth, int minimumHeight)
    {
        Queue<BoundsInt> roomsBoundsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsBoundsList = new List<BoundsInt>();

        roomsBoundsQueue.Enqueue(spaceToSplit);
        while (roomsBoundsQueue.Count > 0)
        {
            var room = roomsBoundsQueue.Dequeue();
            if (room.size.y >= minimumHeight && room.size.x >= minimumWidth)
            {
                if (Random.value < 0.5f) // Split Horizontally
                {
                    if (room.size.y >= minimumHeight * 2)
                        SplitHorizontally(minimumHeight, roomsBoundsQueue, room);

                    else if (room.size.x >= minimumWidth * 2)
                        SplitVertically(minimumWidth, roomsBoundsQueue, room);

                    else if (room.size.x >= minimumWidth && room.size.y >= minimumHeight)
                        roomsBoundsList.Add(room);
                }
                else // Split it Vertically
                {
                    if (room.size.x >= minimumWidth * 2)
                        SplitVertically(minimumWidth, roomsBoundsQueue, room);

                    else if (room.size.y >= minimumHeight * 2)
                        SplitHorizontally(minimumHeight, roomsBoundsQueue, room);

                    else if (room.size.x >= minimumWidth && room.size.y >= minimumHeight)
                        roomsBoundsList.Add(room);
                }
            }
        }

        return roomsBoundsList;
    }

    private static void SplitVertically(int minimumWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minimumHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}
