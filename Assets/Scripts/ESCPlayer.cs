using System;
using System.Collections;
using System.Collections.Generic;
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


public class ESCPlayer : MonoBehaviour {
    class Dice
    {
        int count;
        int reserve;
        Symbol.SymbolType[] diceValues;

        public Dice()
        {
            count = 5;
            reserve = 0;
            diceValues = new Symbol.SymbolType[10];
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
                diceValues[i] = Symbol.RollDiceSymbol();
            } 
        }

        public void Print()
        {
            string diceString = "";
            for (var i = 0; i < count; i++)
            {
                diceString += diceValues[i].ToString() + " ";
            }
            Debug.Log(diceString + " ASASD");
        }
    }

    ESCRoom currentroom;
    Dice dice;

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
