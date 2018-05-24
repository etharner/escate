using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RandTest();
        DiceThrowTest();
        RoomTest();
    }

    void RandTest()
    {
        for (var i = 0; i < 10; i++)
        {
            Debug.Log(ESCUtil.Rand.Next(2, 3 + 1));
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

    void RoomTest()
    {
        ESCMaze Maze;
        int PlayersCount = 5;
        List<ESCPlayer> PlayerList;

        Maze = new ESCMaze();
        PlayerList = new List<ESCPlayer>();
        for (var i = 0; i < PlayersCount; i++)
        {
            PlayerList.Add(new ESCPlayer(Maze));
        }

        PlayerList[0].Turn();

        //public List<ActionType> roomAction;
        //public Dictionary<ESCRoom.RoomDirection, List<ActionType>> siblingActions;

        string ra = "";
        foreach (ActionType action in PlayerList[0].roomAction)
        {
            ra += action.ToString() + " ";
            
        }
        Debug.Log(ra);

        ra = "";
        foreach (ESCRoom.RoomDirection dir in PlayerList[0].siblingActions.Keys)
        {
            ra += PlayerList[0].siblingActions.ToString() + " : " + PlayerList[0].siblingActions[dir] + "\n";
        }
        Debug.Log(ra);

    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
