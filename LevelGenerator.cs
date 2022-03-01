using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Room Random Walk Configuration")]
    [SerializeField]
    private int itterations = 1;
    [SerializeField]
    private int walkLength = 8;
    [Header("Space Partitioning Configuration")]
    [SerializeField]
    private Vector2Int startPosition;
    [SerializeField]
    private Vector2Int levelBoundsDimensions;
    [SerializeField]
    private Vector2Int minRoomDimensions;

    public LevelData levelData { get; private set; }
    private void Start()
    {
        //GenerateLevel();
    }

    public LevelData GenerateLevel()
    {
        List<BoundsInt> levelBounds = GetRoomPartitionsInBounds();
        List<RoomData> rooms = GenerateRoomsFromBounds(levelBounds);
        List<CorridorData> corridors = GenerateCorridorsFromRooms(rooms);
        // Create Level Data
        levelData = new LevelData(rooms, corridors);

        return levelData;
    }

    private List<BoundsInt> GetRoomPartitionsInBounds()
    {
        BoundsInt levelBounds = new BoundsInt((Vector3Int)startPosition, (Vector3Int)levelBoundsDimensions);
        List<BoundsInt> roomsBoundsList = BinarySpacePartitioning.RunBinarySpacePartitioning(levelBounds, minRoomDimensions.x, minRoomDimensions.y);

        return roomsBoundsList;
    }

    private List<RoomData> GenerateRoomsFromBounds(List<BoundsInt> roomsBoundsList)
    {
        List<RoomData> roomsList = new List<RoomData>();

        foreach(BoundsInt roomBounds in roomsBoundsList)
        {
            Vector2Int currentPosition = Vector2Int.RoundToInt(roomBounds.center);
            HashSet<Vector2Int> roomFloorPoints = new HashSet<Vector2Int>();

            for(int i = 0; i < itterations; i ++)
            {
                // var path = RandomWalk.RunSimpleRandomWalkInBounds(currentPosition, walkLength, roomBounds);
                var path = RandomWalk.RunSimpleRandomWalk(currentPosition, walkLength);
                roomFloorPoints.UnionWith(path);
                currentPosition = roomFloorPoints.ElementAt(UnityEngine.Random.Range(0, roomFloorPoints.Count));
            } 
            roomsList.Add(new RoomData(roomFloorPoints, Vector2Int.RoundToInt(roomBounds.center)));
        }    
        return roomsList;
    }

    private List<CorridorData> GenerateCorridorsFromRooms(List<RoomData> roomsList)
    {
        List<CorridorData> corridorsList = new List<CorridorData>();

        RoomData currentRoom = roomsList[Random.Range(0, roomsList.Count)];
        roomsList.Remove(currentRoom);

        while(roomsList.Count > 0)
        {
            RoomData closestRoom = FindClosestRoomTo(currentRoom, roomsList);

            Vector2Int currentRoomConnectPoint = GetRoomConnectPoint(currentRoom, closestRoom);
            Vector2Int closestRoomConnectPoint = GetRoomConnectPoint(closestRoom, currentRoom);

            roomsList.Remove(closestRoom);

            HashSet<Vector2Int> newCorridor = CreateCorridor(closestRoomConnectPoint, closestRoomConnectPoint);
            corridorsList.Add(new CorridorData(currentRoomConnectPoint, closestRoomConnectPoint, newCorridor));

            currentRoom = closestRoom;
        }

        return corridorsList;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int fromRoomPoint, Vector2Int toRoomPoint)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = fromRoomPoint;

        corridor.Add(position);
        while (position.y != toRoomPoint.y)
        {
            if (toRoomPoint.y > position.y)
                position += Vector2Int.up;
            else if (toRoomPoint.y < position.y)
                position += Vector2Int.down;

            corridor.Add(position);
        }
        while (position.x != toRoomPoint.x)
        {
            if (toRoomPoint.x > position.x)
                position += Vector2Int.right;
            else if (toRoomPoint.x < position.x)
                position += Vector2Int.left;

            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int GetRoomConnectPoint(RoomData fromRoom, RoomData toRoom)
    {
        Vector2Int closestPoint = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach(Vector2Int point in fromRoom.FloorPositions)
        {
            float currentDistance = Vector2.Distance(point, toRoom.RoomCenter);
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closestPoint = point;
            }
        }

        fromRoom.FloorPositions.Remove(closestPoint);
        return closestPoint;
    }

    private RoomData FindClosestRoomTo(RoomData currentRoom, List<RoomData> roomsList)
    {
        RoomData closestRoom = null;
        float distance = float.MaxValue;

        foreach(RoomData room in roomsList)
        {
            float currentDistance = Vector2.Distance(room.RoomCenter, currentRoom.RoomCenter);
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closestRoom = room; 
            }
        }

        return closestRoom;
    }
}
