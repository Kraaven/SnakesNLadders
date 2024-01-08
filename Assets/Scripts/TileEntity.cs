using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class TileEntity : MonoBehaviour
{
    public int TileNum;
    public int ResultTile;
    public Spline path;
    enum TileState
    {
        None,
        Ladder,
        Snake,
        Result
    }

    [SerializeField] private TileState state;

    public void Start()
    {
        state = TileState.None;
    }

    public void TileAction(PlayerController piece)
    {
        switch (state)
        {
            case TileState.None:
                Debug.Log("Action: None");
                break;
            case TileState.Result:
                break;
            case TileState.Ladder:
                Debug.Log("Ladder Triggered from location: "+ TileNum);
                piece.MovePath(path,ResultTile);
                break;
            case TileState.Snake:
                break;
        }
    }

    public void SetState(int given)
    {
        Debug.Log(TileNum+ " Tile -> "+ (ResultTile+1));
        switch (given)
        {
            case 1:
                state = TileState.Ladder;
                Debug.Log("Tile: "+ TileNum+ " has been set to Ladder Point with result set to: "+ (ResultTile+1));
                break;
            case 2:
                state = TileState.Snake;
                Debug.Log("Tile: "+ TileNum+ " has been set to Snake Point with result set to: "+ (ResultTile+1));
                break;
        }
    }
    
    
}
