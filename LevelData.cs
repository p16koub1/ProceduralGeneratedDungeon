using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData 
{
    public List<RoomData> RoomsDataList { get; private set; }
    public List<CorridorData> CorridorsDataList { get; private set; }

    public LevelData(List<RoomData> roomsDataList, List<CorridorData> corridorsDataList)
    {
        RoomsDataList = roomsDataList;
        CorridorsDataList = corridorsDataList;
    }
}
