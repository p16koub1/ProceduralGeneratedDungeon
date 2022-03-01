using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelGenerator))]
public class LevelBuilder : MonoBehaviour
{
    [Header("Object Prefabs")]
    [SerializeField]
    private GameObject roomPrefab;
    [SerializeField]
    private GameObject corridorPrefab;
    [Header("Tile Prefabs")]
    [SerializeField]
    private GameObject floorPrefab;

    private LevelGenerator _levelGenerator;
    
    void Start()
    {
        _levelGenerator = GetComponent<LevelGenerator>();
        BuildLevel();
    }

    public void BuildLevel()
    {
        var level = _levelGenerator.GenerateLevel();
        var roomsDataList = level.RoomsDataList;
        var corridorsDataList = level.CorridorsDataList;
        Debug.Log(roomsDataList.Count);

        foreach(var room in roomsDataList)
            BuildRoom(room);
        foreach(var corridor in corridorsDataList)
            BuildCorridor(corridor);

    }

    private void BuildRoom(RoomData roomData)
    {
        GameObject room = Instantiate(roomPrefab, (Vector2)roomData.RoomCenter, Quaternion.identity);
        List<GameObject> roomFloorsList = BuildFloorFromPointList(roomData.FloorPositions);

        foreach(GameObject floor in roomFloorsList)
        {
            floor.transform.SetParent(room.transform);
        }
    }

    private void BuildCorridor(CorridorData corridorData)
    {
        GameObject corridor = Instantiate(roomPrefab, (Vector2)corridorData.FromTile, Quaternion.identity);
        List<GameObject> roomFloorsList = BuildFloorFromPointList(corridorData.Path);

        foreach (GameObject floor in roomFloorsList)
        {
            floor.transform.SetParent(corridor.transform);
        }
    }

    private List<GameObject> BuildFloorFromPointList(HashSet<Vector2Int> points)
    {
        List<GameObject> floors = new List<GameObject>();
        
        foreach (Vector2Int point in points)
        {
            GameObject floor = Instantiate(floorPrefab, (Vector2)point, Quaternion.identity);
            floors.Add(floor);
        }

        return floors;
    }



}
