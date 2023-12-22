using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GenerateVoard GameBoard;
    public void StartGame()
    {
        
        GameBoard = gameObject.GetComponent<GenerateVoard>();
        var playercount = GameBoard.PlayerNos;
        foreach (PlayerController Piece in GameBoard.PlayerArray)
        {
            for (int i = 0; i < playercount; i++)
            {
                GameBoard.PlayerArray[i].PlaceIntoTile(0);
            }
        }
    }
}
