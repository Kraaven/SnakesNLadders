using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class gameController : MonoBehaviour
{
    public GenerateVoard GameBoard;
    public int playercount;
    public bool gamestate;
    public void StartGame(GenerateVoard Tiles)
    {

        GameBoard = Tiles;
        playercount = Tiles.PlayerNos;

        StartCoroutine(PlayingGame());
    }

    IEnumerator PlayingGame()
    {
        foreach (var Piece in GameBoard.PlayerArray)
        {
            Piece.Controller = this;
        }
        gamestate = true;
        while (gamestate)
        {
            foreach (var Piece in GameBoard.PlayerArray)
            {
                int roll = Random.Range(1, 7);
                Debug.Log(Piece.PlayerID+" Player Has rolled "+ roll);
                Piece.AddPosition(roll);
                
                yield return new WaitForSeconds(0.1f);
            }
            
            
        }
    }
}
