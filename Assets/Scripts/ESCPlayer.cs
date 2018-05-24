using System;
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
    
    ESCRoom currentRoom;
    ESCDice dice;
    ESCMaze maze;
    Dictionary<ESCRoom, List<ActionType>> roomActions;
    ESCCondition discoverCondition;

    public ESCPlayer(ESCMaze m)
    {
        dice = new ESCDice();
        maze = m;
        currentRoom = m.Entrance;
        roomActions = new Dictionary<ESCRoom, List<ActionType>>();
        discoverCondition = new ESCCondition(m, ESCCondition.ConditionType.Discover);
    }

    void AddAction(ESCRoom room)
    {
        ActionType action = room.EnterCondition.JudgeActions(dice.DiceValues);
        if (action != ActionType.None)
        {
            roomActions[room].Add(action);
        }
    }

    public void Turn()
    {
        dice.Throw();

        roomActions[currentRoom] = new List<ActionType>() {
            ActionType.None,
            currentRoom.InnerCondition.JudgeActions(dice.DiceValues),
            discoverCondition.JudgeActions(dice.DiceValues)
        };

        if (currentRoom.Parent != null)
        {
            AddAction(currentRoom.Parent);
        }

        foreach (ESCRoom r in currentRoom.Siblings ?? null)
        {
            AddAction(r);
        }
    }
}
