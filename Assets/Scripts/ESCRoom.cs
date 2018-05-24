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

    public enum RoomDirection
    {
        Left,
        Right,
        Top,
        Bottom
    }

    ESCMaze maze;
    public Dictionary<RoomDirection, ESCRoom> Siblings { get; set; }
    public int SiblingsAlive;
    public RoomType type; //public
    public ESCCondition EnterCondition;
    public ESCCondition InnerCondition;
    
	public ESCRoom(ESCMaze m, RoomType roomType)
    {
        maze = m;
        type = roomType;
        EnterCondition = new ESCCondition(maze, ESCCondition.ConditionType.Enter);
        SiblingsAlive = 0;

        Siblings = new Dictionary<RoomDirection, ESCRoom>();
        switch (roomType)
        {
            case RoomType.Entrance:
                Siblings = new Dictionary<RoomDirection, ESCRoom>()
                {
                    { RoomDirection.Left, null },
                    { RoomDirection.Right, null }
                };
                InnerCondition = null;
                break;

            case RoomType.Normal:
                InnerCondition = null;
                break;

            case RoomType.MinorLock:
                InnerCondition = new ESCCondition(maze, ESCCondition.ConditionType.MinorLock);
                break;

            case RoomType.BigLock:
                InnerCondition = new ESCCondition(maze, ESCCondition.ConditionType.BigLock);
                break;

            case RoomType.Exit:
                InnerCondition = new ESCCondition(maze, ESCCondition.ConditionType.Exit);
                break;
        }
    }

    public void GenerateDirectionPockets(RoomDirection parentDirection)
    {
        Siblings = new Dictionary<RoomDirection, ESCRoom>();

        var roomDirections = new List<RoomDirection>(Enum.GetValues(typeof(RoomDirection)).Cast<RoomDirection>().ToList());
        roomDirections.Remove(parentDirection);
        Siblings.Add(parentDirection, null);

        int siblingsCount = ESCUtil.Rand.Next(2, 3 + 1);
        for (var i = 0; i < siblingsCount; i++)
        {
            RoomDirection dir = roomDirections[0];
            roomDirections.RemoveAt(0);
            Siblings.Add(dir, null);
        }
    }

    //public List<RoomDirection> DirectionsAvailable()
    //{
    //    var dirAvailable = new List<RoomDirection>();
    //    for (var key in )
    //}

    public void InsertSibling(RoomDirection dir)
    {
        ESCRoom newRoom = maze.RequestRoom();
        if (newRoom.type != RoomType.Exit)
        {
            newRoom.GenerateDirectionPockets(dir);
        }
        Siblings[dir] = newRoom;
        SiblingsAlive++;
    }
}
