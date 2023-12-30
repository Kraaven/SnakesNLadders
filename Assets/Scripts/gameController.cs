using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class gameController : MonoBehaviour
{
    public GenerateVoard GameBoard;
    public int playercount;
    public bool gamestate;
    public GenerateLadder LPrefab;

    private List<Vector2> Ladders;
    private bool intersected;
    public void StartGame(GenerateVoard Tiles)
    {

        GameBoard = Tiles;
        playercount = Tiles.PlayerNos;
        

        StartCoroutine(PlayingGame());
        GenerateLadders(Random.Range(7,13));
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
                
                yield return new WaitForSeconds(1f);
            }
            
            
        }
    }

    private void GenerateLadders(int lads)
    {
        List<Vector2> Ladders = new List<Vector2>();
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
                    Debug.Log(Tile1.GetComponent<TileEntity>().TileNum + " Tile Selected");
                } while (Tile1.GetComponent<TileEntity>().TileNum % 10 == 0 ||
                         Tile1.GetComponent<TileEntity>().TileNum % 10 == 1);

                TilePos = Random.Range(0, 4);
                Debug.Log("Chosen Height: " + TilePos);
                int add;
                switch (TilePos)
                {
                    case 0:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        add = (10 - (TilePos % 10)) * 2;
                        Debug.Log(add + " was added");
                        TilePos += add;
                        Debug.Log("Chosen Target Tile: " + TilePos);
                        break;
                    case 1:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        TilePos += 19;
                        Debug.Log("Chosen Target Tile: " + TilePos);
                        break;
                    case 2:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        add = (10 - (TilePos % 10)) * 2;
                        Debug.Log(add + " was added");
                        TilePos += add;
                        TilePos += 20;
                        Debug.Log("Chosen Target Tile: " + TilePos);
                        break;
                    case 3:
                        TilePos = Tile1.GetComponent<TileEntity>().TileNum;
                        TilePos += 39;
                        Debug.Log("Chosen Target Tile: " + TilePos);
                        break;
                }

                int Row = TilePos / 10;
                Debug.Log("Tile in "+ TilePos+ " belongs in the "+ (Row*10));
                int oriTilePos = TilePos;
                do
                {
                    TilePos = oriTilePos;
                    Debug.Log("original Tile is "+ TilePos);
                    int offset = Random.Range(1, 4);
                    Debug.Log("Chosen Offset : " + offset);
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            Debug.Log("Added: "+ offset);
                            TilePos += offset;
                            break;
                        case 1:
                            Debug.Log("Subtracted:"+ offset);
                            TilePos -= offset;
                            break;
                    }
                } while (TilePos / 10 != Row);

                Vector2 Tilepair = new Vector2(Tile1.GetComponent<TileEntity>().TileNum, TilePos);
                if (Ladders.Count != 0 && TilePos < 99)
                {
                    foreach (var Ladder in Ladders)
                    {
                        if (Tile1 == GameBoard.TileArray[(int)Ladder.x] || Tile1 == GameBoard.TileArray[(int)Ladder.y] ||GameBoard.TileArray[TilePos] == GameBoard.TileArray[(int)Ladder.x] || GameBoard.TileArray[TilePos] == GameBoard.TileArray[(int)Ladder.y]  )
                        {
                            intersected = true;
                            Debug.Log("Intersection via same Tiles");
                            break;
                        }

                        Vector2 A1pos = Tile1.transform.position;
                        Vector2 A2pos = GameBoard.TileArray[TilePos].transform.position;
                        Vector2 B1pos = GameBoard.TileArray[(int)Ladder.x].transform.position;
                        Vector2 B2pos = GameBoard.TileArray[(int)Ladder.y].transform.position;
                        Debug.Log("Line 1: "+ A1pos + " to "+ A2pos);
                        Debug.Log("Line 2: "+ B1pos + " to "+ B2pos);
                        if (lineSegmentsIntersect(A1pos, A2pos, B1pos, B2pos))
                        {
                            intersected = true;
                            Debug.Log("Intersection via crossed ladders");
                            break;
                        }
                    }
                    
                    Debug.Log(intersected);
                }


            } while (TilePos > 98 || intersected);
            
            
            
            
            Debug.Log("Final Target Tile: "+ TilePos);

            
            Tile2 = GameBoard.TileArray[TilePos];
            
            GenerateLadder templadder = Instantiate(LPrefab);
            templadder.InitLadder(Tile1,Tile2);
            Ladders.Add(new Vector2(Tile1.GetComponent<TileEntity>().TileNum -1,Tile2.GetComponent<TileEntity>().TileNum-1));
            Debug.Log("------Ladder generated------");
        }
    }

    public static bool lineSegmentsIntersect(Vector2 lineOneA, Vector2 lineOneB, Vector2 lineTwoA, Vector2 lineTwoB)
    {
        return (((lineTwoB.y - lineOneA.y) * (lineTwoA.x - lineOneA.x) > (lineTwoA.y - lineOneA.y) * (lineTwoB.x - lineOneA.x)) != ((lineTwoB.y - lineOneB.y) * (lineTwoA.x - lineOneB.x) > (lineTwoA.y - lineOneB.y) * (lineTwoB.x - lineOneB.x)) && ((lineTwoA.y - lineOneA.y) * (lineOneB.x - lineOneA.x) > (lineOneB.y - lineOneA.y) * (lineTwoA.x - lineOneA.x)) != ((lineTwoB.y - lineOneA.y) * (lineOneB.x - lineOneA.x) > (lineOneB.y - lineOneA.y) * (lineTwoB.x - lineOneA.x)));
    }
}
