using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RandTest();
        DiceThrowTest();
    }

    void RandTest()
    {
        for (var i = 0; i < 10; i++)
        {
            Debug.Log(ESCUtil.rand.Next(2, 3 + 1));
        }
    }

    void DiceThrowTest()
    {
        ESCDice d = new ESCDice();
        for (var i = 0; i < 5; i++)
        {
            d.Throw();
            d.Print();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
