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
        gamestate = true;
        while (gamestate)
        {
            int chosen = Random.Range(0, playercount);
            GameBoard.PlayerArray[chosen].AddPosition(1);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
