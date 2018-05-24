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

    public static SymbolType RollDiceSymbol()
    {
        var diceSymbols = new List<SymbolType>();
        diceSymbols.AddRange(Enum.GetValues(typeof(SymbolType)).Cast<SymbolType>().ToList());
        diceSymbols.Add(SymbolType.Human);

        return diceSymbols[ESCUtil.Rand.Next(diceSymbols.Count)];
    }

    public static SymbolType RollLockCondition()
    {
        var lockSymbols = new List<SymbolType>() { SymbolType.Key, SymbolType.Torch };
        return lockSymbols[ESCUtil.Rand.Next(lockSymbols.Count)];
    }

    public static SymbolType RollCondition()
    {
        SymbolType[] conditions = { SymbolType.Human, SymbolType.Torch, SymbolType.Key };
        return conditions[ESCUtil.Rand.Next(conditions.Length)];
    }

}