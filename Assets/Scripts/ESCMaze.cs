using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCMaze {
    ESCRoom startRoom;

    public int Gems;
    public ESCRoom Entrance { get; set; }
    Dictionary<ESCRoom.RoomType, List<ESCRoom>> roomsPool;
    static Dictionary<ESCRoom.RoomType, int> roomsCount = new Dictionary<ESCRoom.RoomType, int>()
    {
        { ESCRoom.RoomType.Normal, 7 },
        { ESCRoom.RoomType.MinorLock, 8 },
        { ESCRoom.RoomType.BigLock, 4 },
        { ESCRoom.RoomType.Exit, 1 }
    };

    public ESCMaze() {
        Gems = CalcGems();
        roomsPool = new Dictionary<ESCRoom.RoomType, List<ESCRoom>>();
        foreach (ESCRoom.RoomType rt in roomsCount.Keys)
        {
            if (!roomsPool.ContainsKey(rt))
            {
                roomsPool[rt] = new List<ESCRoom>();
            }
            roomsPool[rt].Add(new ESCRoom(this, rt));
        }
        Entrance = new ESCRoom(this, ESCRoom.RoomType.Entrance);
	}

    int CalcGems()
    {
        int[] gemsCount = { 9, 9, 13, 16, 18 };

        return gemsCount[GameObject.FindObjectOfType<ESCEngine>().PlayersCount - 1];
    }

    public ESCRoom RequestRoom()
    {
        var keyList = new List<ESCRoom.RoomType>(roomsPool.Keys);
        ESCRoom.RoomType key = keyList[ESCUtil.Rand.Next(0, keyList.Count)];
        ESCRoom ejectedRoom = roomsPool[key][0];
        roomsPool[key].RemoveAt(0);
        if (roomsPool[key].Count == 0)
        {
            roomsPool.Remove(key);
        }

        return ejectedRoom;
    }
}
