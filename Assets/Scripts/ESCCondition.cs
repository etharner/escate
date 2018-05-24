using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCCondition
{
    public enum ConditionType
    {
        Discover,
        Enter,
        MinorLock,
        BigLock,
        Exit
    }

    ESCMaze maze;
    ConditionType type;
    Symbol.SymbolType[] conditionElements;

    public ESCCondition(ESCMaze m, ConditionType conditionType)
    {
        maze = m;
        type = conditionType;

        Symbol.SymbolType lockCondition = Symbol.RollLockCondition();
        switch (type)
        {
            case ConditionType.Discover:
                conditionElements = new Symbol.SymbolType[2] { Symbol.SymbolType.Human, Symbol.SymbolType.Human };
                break;

            case ConditionType.Enter:
                conditionElements = new Symbol.SymbolType[2];
                for (var i = 0; i < 2; i++)
                {
                    conditionElements[i] = Symbol.RollCondition();
                }
                break;

            case ConditionType.MinorLock:
            case ConditionType.BigLock:
                conditionElements = new Symbol.SymbolType[4];
                for (var i = 0; i < 4; i++)
                {
                    conditionElements[i] = lockCondition;
                }
                break;

            case ConditionType.Exit:
                conditionElements = CalcExitCondition();
                break;
        }
    }

    public Symbol.SymbolType[] ConditionElements
    {
        get
        {
            if (type == ConditionType.Exit)
            {
                return CalcExitCondition();
            }
            return conditionElements;
        }
    }

    Symbol.SymbolType[] CalcExitCondition()
    {
        conditionElements = new Symbol.SymbolType[maze.Gems + 1];
        for (var i = 0; i < conditionElements.Length; i++)
        {
            conditionElements[i] = Symbol.SymbolType.Key;
        }
        return conditionElements;
    }

    bool Resolve(Symbol.SymbolType[] dice)
    {
        var diceSymbols = new Dictionary<Symbol.SymbolType, int>();
        var conditionSymbols = new Dictionary<Symbol.SymbolType, int>();

        foreach (Symbol.SymbolType s in dice)
        {
            ++diceSymbols[s];
        }

        foreach (Symbol.SymbolType s in conditionElements)
        {
            ++conditionSymbols[s];
        }

        foreach (Symbol.SymbolType s in Enum.GetValues(typeof(Symbol.SymbolType)))
        {
            if (conditionSymbols[s] > 0 && diceSymbols[s] < conditionSymbols[s])
            {
                return false;
            }
        }

        return true;
    }

    //validate dice content (if many players)
    public ActionType JudgeActions(Symbol.SymbolType[] dice)
    {
        bool verdict = Resolve(dice);

        if (verdict)
        {
            switch (type)
            {
                case ConditionType.Discover:
                    return ActionType.Discover;

                case ConditionType.Enter:
                    return ActionType.Move;

                case ConditionType.MinorLock:
                    return ActionType.OpenMinorLock;

                case ConditionType.BigLock:
                    Symbol.SymbolType type = conditionElements[0];
                    int typeCount = 0;
                    foreach (var s in dice)
                    {
                        if (s == type)
                        {
                            ++typeCount;
                        }
                    }

                    if (typeCount >= 10)
                    {
                        return ActionType.OpenBigLock;
                    }

                    if (typeCount >= 7)
                    {
                        return ActionType.OpenNormalLock;
                    }

                    return ActionType.OpenMinorLock;

                case ConditionType.Exit:
                    return ActionType.Exit;
            }
        }

        return ActionType.None;
    }
}