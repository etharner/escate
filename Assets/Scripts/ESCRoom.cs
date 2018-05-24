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

    public ESCRoom Parent { get; set; }
    public ESCRoom[] Siblings { get; }
    RoomType type;
    public ESCCondition EnterCondition;
    public ESCCondition InnerCondition;
    
	public ESCRoom(ESCMaze maze, RoomType roomType)
    {
        Parent = null;
        type = roomType;
        EnterCondition = new ESCCondition(maze, ESCCondition.ConditionType.Enter);

        switch (roomType)
        {
            case RoomType.Entrance:
                Siblings = new ESCRoom[2];
                break;

            case RoomType.MinorLock:
                Siblings = new ESCRoom[ESCUtil.rand.Next(2, 3 + 1)];
                InnerCondition = new ESCCondition(maze, ESCCondition.ConditionType.MinorLock);
                break;

            case RoomType.BigLock:
                Siblings = new ESCRoom[ESCUtil.rand.Next(2, 3 + 1)];
                InnerCondition = new ESCCondition(maze, ESCCondition.ConditionType.BigLock);
                break;

            case RoomType.Exit:
                InnerCondition = new ESCCondition(maze, ESCCondition.ConditionType.Exit);
                break;
        }
    }
}
