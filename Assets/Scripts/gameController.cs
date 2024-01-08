using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class gameController : MonoBehaviour
{
    public GenerateVoard GameBoard;
    public int playercount;
    public bool gamestate;
    public GenerateLadder LPrefab;
    public GameObject Sprefab;
    public int Climbspeed;

    private List<Vector2> Ladders;
    private List<Vector2> Snakes;
    private bool intersected;
    

    public void StartGame(GenerateVoard Tiles)
    {
        GameBoard = Tiles;
        playercount = Tiles.PlayerNos;
        
        GenerateLadders(Random.Range(7,13));
        GenerateSnakes(Random.Range(6,9));
        //GenerateSnakes(1);
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
            for (int i = 0; i < playercount && gamestate; i++)
            {
                var Piece = GameBoard.PlayerArray[i];
                int roll = Random.Range(1, 7);
                Piece.AddPosition(roll);
                
                yield return new WaitForSeconds(2.25f);
            }
            
            
        }
    }

    private void GenerateLadders(int lads)
    {
        Ladders = new List<Vector2>();
        for (int i = 0; i < lads; i++)
        {
            GameObject Tile1;
            GameObject Tile2;
            int TilePos = 0;

            do
            {
                intersected = false;
                do
                {
                    Tile1 = GameBoard.TileArray[Random.Range(3, 88)];
                } while (Tile1.GetComponent<TileEntity>().TileNum % 10 == 0 ||
                         Tile1.GetComponent<TileEntity>().TileNum % 10 == 1);

                TilePos = Random.Range(0, 4);
                int add;
                switch (TilePos)
                {
                    case 0:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        add = (10 - (TilePos % 10)) * 2;
                        TilePos += add;
                        break;
                    case 1:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        TilePos += 19;
                        break;
                    case 2:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        add = (10 - (TilePos % 10)) * 2;
                        TilePos += add;
                        TilePos += 20;
                        break;
                    case 3:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        TilePos += 39;
                        break;
                }

                int Row = TilePos / 10;
                int oriTilePos = TilePos;
                do
                {
                    TilePos = oriTilePos;
                    int offset = Random.Range(1, 4);
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            TilePos += offset;
                            break;
                        case 1:
                            TilePos -= offset;
                            break;
                    }
                } while (TilePos / 10 != Row);
                
                if (Ladders.Count != 0 && TilePos < 99)
                {
                    foreach (var Ladder in Ladders)
                    {
                        if (Tile1 == GameBoard.TileArray[(int)Ladder.x] || Tile1 == GameBoard.TileArray[(int)Ladder.y] ||GameBoard.TileArray[TilePos] == GameBoard.TileArray[(int)Ladder.x] || GameBoard.TileArray[TilePos] == GameBoard.TileArray[(int)Ladder.y]  )
                        {
                            intersected = true;
                            break;
                        }

                        Vector2 A1pos = Tile1.transform.position;
                        Vector2 A2pos = GameBoard.TileArray[TilePos].transform.position;
                        Vector2 B1pos = GameBoard.TileArray[(int)Ladder.x].transform.position;
                        Vector2 B2pos = GameBoard.TileArray[(int)Ladder.y].transform.position;
                        if (lineSegmentsIntersect(A1pos, A2pos, B1pos, B2pos))
                        {
                            intersected = true;
                            break;
                        }
                    }
                    
                }


            } while (TilePos > 98 || intersected);

            Tile2 = GameBoard.TileArray[TilePos];
            
            GenerateLadder templadder = Instantiate(LPrefab);
            templadder.InitLadder(Tile1,Tile2);
            Ladders.Add(new Vector2(Tile1.GetComponent<TileEntity>().TileNum -1,Tile2.GetComponent<TileEntity>().TileNum-1));
        }
        
        SetupLadders();
        Debug.Log("ladders setup for: "+ Ladders.Count+ " Ladders");
    }

    public static bool lineSegmentsIntersect(Vector2 lineOneA, Vector2 lineOneB, Vector2 lineTwoA, Vector2 lineTwoB)
    {
        return (((lineTwoB.y - lineOneA.y) * (lineTwoA.x - lineOneA.x) > (lineTwoA.y - lineOneA.y) * (lineTwoB.x - lineOneA.x)) != ((lineTwoB.y - lineOneB.y) * (lineTwoA.x - lineOneB.x) > (lineTwoA.y - lineOneB.y) * (lineTwoB.x - lineOneB.x)) && ((lineTwoA.y - lineOneA.y) * (lineOneB.x - lineOneA.x) > (lineOneB.y - lineOneA.y) * (lineTwoA.x - lineOneA.x)) != ((lineTwoB.y - lineOneA.y) * (lineOneB.x - lineOneA.x) > (lineOneB.y - lineOneA.y) * (lineTwoB.x - lineOneA.x)));
    }

    private void SetupLadders()
    {
        Debug.Log("Setting Up Ladders");
        foreach (var ladder in Ladders)
        {
            GameBoard.TileArray[(int)ladder.x].GetComponent<TileEntity>().ResultTile = (int)ladder.y;
            GameBoard.TileArray[(int)ladder.x].GetComponent<TileEntity>().SetState(1);
        }
    }

    private void GenerateSnakes(int sneks)
    {
        Snakes = new List<Vector2>();
        for (int i = 0; i < sneks; i++)
        {
            GameObject Tile1;
            GameObject Tile2;
            int TilePos = 0;

            do
            {
                intersected = false;
                do
                {
                    Tile1 = GameBoard.TileArray[Random.Range(3, 88)];
                } while (Tile1.GetComponent<TileEntity>().TileNum % 10 == 0 ||
                         Tile1.GetComponent<TileEntity>().TileNum % 10 == 1);

                TilePos = Random.Range(0, 4);
                int add;
                switch (TilePos)
                {
                    case 0:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        add = (10 - (TilePos % 10)) * 2;
                        TilePos += add;
                        break;
                    case 1:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        TilePos += 19;
                        break;
                    case 2:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        add = (10 - (TilePos % 10)) * 2;
                        TilePos += add;
                        TilePos += 20;
                        break;
                    case 3:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        TilePos += 39;
                        break;
                }

                int Row = TilePos / 10;
                int oriTilePos = TilePos;
                do
                {
                    TilePos = oriTilePos;
                    int offset = Random.Range(1, 4);
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            TilePos += offset;
                            break;
                        case 1:
                            TilePos -= offset;
                            break;
                    }
                } while (TilePos / 10 != Row);
                
                if (Snakes.Count != 0 && TilePos < 99)
                {
                    foreach (var snake in Snakes)
                    {
                        //Check if snakes share the points with other snakes
                        if (Tile1 == GameBoard.TileArray[(int)snake.x] || Tile1 == GameBoard.TileArray[(int)snake.y] ||GameBoard.TileArray[TilePos] == GameBoard.TileArray[(int)snake.x] || GameBoard.TileArray[TilePos] == GameBoard.TileArray[(int)snake.y])
                        {
                            intersected = true;
                            break;
                        }
                        
                        //check if snakes share their points with ladders
                        
                        // foreach (var ladder in Ladders)
                        // {
                        //     if (GameBoard.TileArray[(int)ladder.x] == GameBoard.TileArray[(int)snake.x] ||
                        //         GameBoard.TileArray[(int)ladder.x] == GameBoard.TileArray[(int)snake.y] ||
                        //         GameBoard.TileArray[(int)ladder.y] == GameBoard.TileArray[(int)snake.x] ||
                        //         GameBoard.TileArray[(int)ladder.y] == GameBoard.TileArray[(int)snake.y])
                        //     {
                        //         intersected = true;
                        //         break; 
                        //     }   
                        // }
                    }
                    
                }


            } while (TilePos > 98 || intersected);

            Tile2 = GameBoard.TileArray[TilePos];
            
            GameObject tempsnake = Instantiate(Sprefab,new Vector3(0,0,0),Quaternion.identity);
            tempsnake.GetComponent<InitSnake>().initSnake(Tile2,Tile1,Random.Range(3,7));
            Snakes.Add(new Vector2(Tile1.GetComponent<TileEntity>().TileNum -1,Tile2.GetComponent<TileEntity>().TileNum-1));
        }
        
        //SetupSnakes();
        Debug.Log("Snakes setup for: "+ Snakes.Count+ " sneks");
    }
    
    private void SetupSnakes()
    {
        Debug.Log("Setting Up Snakes");
        foreach (var snek in Snakes)
        {
            GameBoard.TileArray[(int)snek.y].GetComponent<TileEntity>().ResultTile = (int)snek.y;
            GameBoard.TileArray[(int)snek.y].GetComponent<TileEntity>().SetState(2);
        }
    }
}
