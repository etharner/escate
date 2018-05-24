using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCTest : MonoBehaviour {
    List<ESCPlayer> PlayerList = new List<ESCPlayer>();

	// Use this for initialization
	void Start () {

        //RandTest();
        //DiceThrowTest();
        //RoomTest();
        GemTest();
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

        Maze = new ESCMaze();

        for (var i = 0; i < PlayersCount; i++)
        {
            PlayerList.Add(new ESCPlayer(Maze));
        }

        PlayerList[0].Turn();
        Print(0);
        // public void PerformAction(ESCRoom.RoomDirection dir, ActionType action, int playerId)
        PlayerList[0].PerformAction(ESCRoom.RoomDirection.Left, ActionType.Discover, -1);
        Debug.Log("TYPE : " + PlayerList[0].currentRoom.Siblings[ESCRoom.RoomDirection.Left].type.ToString());
        PlayerList[0].PerformAction(ESCRoom.RoomDirection.Left, ActionType.Move, -1);
        PlayerList[0].Turn();
        Print(0);
    }

    void GemTest()
    {
        ESCMaze Maze;
        int PlayersCount = 5;

        Maze = new ESCMaze();

        for (var i = 0; i < PlayersCount; i++)
        {
            PlayerList.Add(new ESCPlayer(Maze));
        }

        PlayerList[0].Turn();
        Print(0);
        // public void PerformAction(ESCRoom.RoomDirection dir, ActionType action, int playerId)
        PlayerList[0].PerformAction(ESCRoom.RoomDirection.Left, ActionType.Discover, -1);
        Debug.Log("TYPE : " + PlayerList[0].currentRoom.Siblings[ESCRoom.RoomDirection.Left].type.ToString());
        PlayerList[0].PerformAction(ESCRoom.RoomDirection.Left, ActionType.Move, -1);
        PlayerList[0].Turn();
        Print(0);
    }

    void Print(int playerId)
    {
        PlayerList[playerId].dice.Print();
        string ra = "Room ";
        foreach (ActionType action in PlayerList[playerId].roomAction)
        {
            ra += action.ToString() + " ";

        }
        Debug.Log(ra);

        ra = "Siblings ";
        foreach (ESCRoom.RoomDirection dir in PlayerList[playerId].siblingActions.Keys)
        {
            ra += dir.ToString() + " : [";
            foreach (ActionType action in PlayerList[playerId].siblingActions[dir])
            {
                ra += action.ToString() + ",";
            }
            ra += "]\n";
        }
        Debug.Log(ra);
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
