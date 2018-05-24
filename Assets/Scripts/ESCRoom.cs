using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ESCRoom {
    public enum RoomType
    {
        Entrance,
        Normal,
        MinorLock,
        BigLock,
        Gem,
        Exit
    }

    public ESCRoom parent { get; set; }
    ESCRoom[] siblings;
    RoomType type;
    ESCCondition enterCondition;
    ESCCondition innerCondition;
    
	public ESCRoom(ESCMaze maze, RoomType roomType)
    {
        parent = null;
        type = roomType;
        enterCondition = new ESCCondition(maze, ESCCondition.ConditionType.Enter);

        switch (roomType)
        {
            case RoomType.MinorLock:
                innerCondition = new ESCCondition(maze, ESCCondition.ConditionType.MinorLock);
                break;

            case RoomType.BigLock:
                innerCondition = new ESCCondition(maze, ESCCondition.ConditionType.BigLock);
                break;

            case RoomType.Exit:
                innerCondition = new ESCCondition(maze, ESCCondition.ConditionType.Exit);
                break;
        }
    }
}
