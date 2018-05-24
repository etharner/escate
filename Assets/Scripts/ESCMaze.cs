using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCMaze {
    ESCRoom startRoom;

    public int Gems;
    ESCRoom entrance;
    Dictionary<ESCRoom.RoomType, List<ESCRoom>> roomsPool;
    static Dictionary<ESCRoom.RoomType, int> roomsCount = new Dictionary<ESCRoom.RoomType, int>()
    {
        { ESCRoom.RoomType.Entrance, 1 },
        { ESCRoom.RoomType.Normal, 7 },
        { ESCRoom.RoomType.MinorLock, 8 },
        { ESCRoom.RoomType.BigLock, 4 },
        { ESCRoom.RoomType.Gem, 1 },
        { ESCRoom.RoomType.Exit, 1 }
    };

    // Use this for initialization
    void Start () {
        roomsPool = new Dictionary<ESCRoom.RoomType, List<ESCRoom>>();
        foreach (ESCRoom.RoomType rt in roomsCount.Keys)
        {
            roomsPool[rt].Add(new ESCRoom(this, rt));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
