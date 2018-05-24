using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCDice
{
    int count;
    int reserve;
    public Symbol.SymbolType[] DiceValues { get; }

    public ESCDice()
    {
        count = 5;
        reserve = 0;
        DiceValues = new Symbol.SymbolType[10];
    }

    public Symbol.SymbolType[] GetRealDice()
    {
        var values = new Symbol.SymbolType[count];
        for (var i = 0; i < count; i++)
        {
            values[i] = DiceValues[i];
        }
        return values;
    }

    public void Degrade(int degradeCount)
    {
        count -= degradeCount;
        reserve += degradeCount;

        if (count < 0)
        {
            Debug.Log("Count below 0");
        }
    }

    public void Destruct()
    {
        --count;
        --reserve;
    }

    public void Upgrade(int upgradeCount)
    {
        if (count < 0)
        {
            count = 1;
            --reserve;
            return;
        }

        for (var i = 0; i < upgradeCount && reserve >= 0; i++)
        {
            ++count;
            --reserve;
        }
    }

    public void Synthesize()
    {
        ++count;
    }

    public void Throw()
    {
        for (var i = 0; i < count; i++)
        {
            DiceValues[i] = Symbol.RollDiceSymbol();
        }
    }

    public void Print()
    {
        string diceString = "";
        for (var i = 0; i < count; i++)
        {
            diceString += DiceValues[i].ToString() + " ";
        }
        Debug.Log(diceString);
    }
}
