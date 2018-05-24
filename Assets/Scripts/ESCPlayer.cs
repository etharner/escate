using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCPlayer : MonoBehaviour {
    class Dice
    {
        int count;
        int reserve;
        Symbol.SymbolType[] symbols;

        Dice()
        {
            count = 5;
        }

        void Degrade(int degradeCount)
        {
            count -= degradeCount;
            reserve += degradeCount;

            if (count < 0)
            {
                Debug.Log("Count below 0");
            }
        }

        void Destruct()
        {
            --count;
            --reserve;
        }

        void Upgrade(int upgradeCount)
        {
            if (count < 0)
            {
                count = 1;
                --reserve;
            }

            for (var i = 0; i < upgradeCount && reserve >= 0; i++)
            {
                ++count;
                --reserve;
            }
        }

        void Synthesize()
        {
            ++count;
        }

        void Throw()
        {
            for (var i = 0; i < count; i++)
            {
                symbols[i] = Symbol.RollSymbol();
            } 
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
