﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ActionType
{
    Discover,
    Move,
    OpenMinorLock,
    OpenNormalLock,
    OpenBigLock,
    Exit,
    None
}

public class ESCPlayer {
    public ESCRoom currentRoom; //public
    public ESCDice dice; //public
    ESCMaze maze;
    public List<ActionType> roomAction;
    public Dictionary<ESCRoom.RoomDirection, List<ActionType>> siblingActions;


    public ESCPlayer(ESCMaze m)
    {
        dice = new ESCDice();
        maze = m;
        currentRoom = m.Entrance;
        siblingActions = new Dictionary<ESCRoom.RoomDirection, List<ActionType>>();
    }

    void AddAction(ESCRoom.RoomDirection dir, ActionType action)
    {
        if (action != ActionType.None)
        {
            if (!siblingActions.ContainsKey(dir))
            {
                siblingActions[dir] = new List<ActionType>();
            }
            siblingActions[dir].Add(action);
        }
    }

    public void Turn()
    {
        ESCCondition discoverCondition;
        discoverCondition = new ESCCondition(maze, ESCCondition.ConditionType.Discover);

        dice.Throw();

        roomAction = new List<ActionType>() {
            ActionType.None
        };
        if (currentRoom.InnerCondition!= null)
        {
            roomAction.Add(currentRoom.InnerCondition.JudgeActions(dice.GetRealDice()));
        }

        foreach (ESCRoom.RoomDirection dir in currentRoom.Siblings.Keys)
        {
            ActionType discoverConditionAction = discoverCondition.JudgeActions(dice.GetRealDice());
            if (currentRoom.Siblings[dir] == null)
            {
                if (discoverConditionAction != ActionType.None)
                {
                    AddAction(dir, discoverConditionAction);
                }
            }
            else
            {
                AddAction(dir, currentRoom.Siblings[dir].EnterCondition.JudgeActions(dice.GetRealDice()));
            }
        }
    }

    //Pushed Button
    public void PerformAction(ESCRoom.RoomDirection dir, ActionType action, int playerId)
    {
        switch (action)
        {
            case ActionType.Discover:
                currentRoom.InsertSibling(dir);
                break;

            case ActionType.Move:
                currentRoom = currentRoom.Siblings[dir];
                break;

            case ActionType.OpenMinorLock:
                --maze.Gems;
                currentRoom.InnerCondition = null;
                break;

            case ActionType.OpenNormalLock:
                maze.Gems -= 2;
                currentRoom.InnerCondition = null;
                break;

            case ActionType.OpenBigLock:
                maze.Gems -= 3;
                currentRoom.InnerCondition = null;
                break;

            case ActionType.Exit:
                List<ESCPlayer> playerList = GameObject.FindObjectOfType<ESCEngine>().PlayerList;
                playerList[playerId].dice.Synthesize();
                playerList.Remove(this);
                break;
        }

        roomAction.Clear();
        siblingActions.Clear();
    }
}
