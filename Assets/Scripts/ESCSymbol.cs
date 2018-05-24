using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public static SymbolType RollDiceSymbol()
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