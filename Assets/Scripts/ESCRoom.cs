using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ActionList
{
    Discover,
    Move,
    OpenLock,

}

public class Symbol
{
    public enum SymbolType
    {
        Human,
        Torch,
        Key,
        Death,
        Gold
    }

    static System.Random rand = new System.Random();

    public static SymbolType RollDiceSymbols()
    {
        var diceSymbols = new List<SymbolType>();
        diceSymbols.AddRange(Enum.GetValues(typeof(SymbolType)).Cast<SymbolType>().ToList());
        diceSymbols.Add(SymbolType.Human);

        return diceSymbols[rand.Next(diceSymbols.Count)];
    }

    public static SymbolType RollLockCondition()
    {
        var lockSymbols = new List<SymbolType>() { SymbolType.Key, SymbolType.Torch };
        return lockSymbols[rand.Next(lockSymbols.Count)];
    }

    public static SymbolType RollCondition()
    {
        SymbolType[] conditions = { SymbolType.Human, SymbolType.Torch, SymbolType.Key };
        return conditions[rand.Next(conditions.Length)];
    }

}

public class ESCCondition
{    
    public enum ConditionType
    {
        Enter,
        Lock,
        LockThree,
        Exit
    }

    ConditionType type;
    Symbol.SymbolType[] conditionsList;
    ESCMaze maze;

    ESCCondition()
    {
        Symbol.SymbolType lockCondition = Symbol.RollLockCondition();
        switch (type)
        {
            case ConditionType.Enter:
                conditionsList = new Symbol.SymbolType[2];
                for (var i = 0; i < 2; i++)
                {
                    conditionsList[i] = Symbol.RollCondition();
                }
                break;

            case ConditionType.Lock:
                conditionsList = new Symbol.SymbolType[4];
                for (var i = 0; i < 4; i++)
                {
                    conditionsList[i] = lockCondition;
                }
                break;

            case ConditionType.LockThree:
                conditionsList = new Symbol.SymbolType[11];
                for (var i = 0; i < 11; i++)
                {
                    conditionsList[i] = lockCondition;
                }
                break;

            case ConditionType.Exit:
                conditionsList = CalcExitCondition();
                break;
        }
    }

    public Symbol.SymbolType[] ConditionsList
    {
        get
        {
            if (type == ConditionType.Exit)
            {
                return CalcExitCondition();
            }
            return conditionsList;
        }
    }

    Symbol.SymbolType[] CalcExitCondition()
    {
        conditionsList = new Symbol.SymbolType[maze.Gems + 1];
        for (var i = 0; i < conditionsList.Length; i++)
        {
            conditionsList[i] = Symbol.SymbolType.Key;
        }
        return conditionsList;
    }

   
    bool Resolve(Symbol.SymbolType[] dice)
    {
        var diceSymbols = new Dictionary<Symbol.SymbolType, int>();
        var conditionSymbols = new Dictionary<Symbol.SymbolType, int>();

        foreach (Symbol.SymbolType s in dice)
        {
            ++diceSymbols[s];
        }

        foreach (Symbol.SymbolType s in conditionsList)
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

}

public class ESCRoom : MonoBehaviour {
    ESCRoom[] siblings;

    
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
