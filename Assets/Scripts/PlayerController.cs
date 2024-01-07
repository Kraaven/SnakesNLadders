using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerController : MonoBehaviour
{
    public GenerateVoard Board;
    public int PlayerID;
    public int PlayerPosition;
    public gameController Controller;

    public void initPlayer(int pos, GenerateVoard Tiles)
    {
        if (pos > 4)
        {
            Debug.Log("Player not registered");
        }
        else
        {
            switch (pos)
            {
                case 1:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case 2:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case 3:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                case 4:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
                
            }

            PlayerID = pos;
            Board = Tiles;
            PlaceIntoTile(0);
        }
    }

    public void PlaceIntoTile(int Id)
    {
        GameObject tile = Board.TileArray[Id];
        if (tile.transform.GetChild(1).childCount == 0)
        {
            gameObject.transform.parent = tile.transform.GetChild(1).transform;
            gameObject.transform.position = new Vector3(gameObject.transform.parent.position.x,
                gameObject.transform.parent.position.y, -1);
        }
        else
        {
            if (tile.transform.GetChild(2).childCount == 0)
            {
                gameObject.transform.parent = tile.transform.GetChild(2).transform;
                gameObject.transform.position = new Vector3(gameObject.transform.parent.position.x,
                    gameObject.transform.parent.position.y, -1);
            }
            else
            {
                if (tile.transform.GetChild(3).childCount == 0)
                {
                    gameObject.transform.parent = tile.transform.GetChild(3).transform;
                    gameObject.transform.position = new Vector3(gameObject.transform.parent.position.x,
                        gameObject.transform.parent.position.y, -1);
                }
                else
                {
                    if (tile.transform.GetChild(4).childCount == 0)
                    {
                        gameObject.transform.parent = tile.transform.GetChild(4).transform;
                        gameObject.transform.position = new Vector3(gameObject.transform.parent.position.x,
                            gameObject.transform.parent.position.y, -1);
                    }
                    else
                    {
                        Debug.Log("Player cant be placed " + PlayerID);
                    }
                }  
            }
        }

        PlayerPosition = Id;

    }

    public void AddPosition(int Am)
    {
        PlayerPosition += Am;

        switch (PlayerPosition)
        {
            case < 99:
                StartCoroutine(Move(Am));
                break;
            case >99:
                PlayerPosition -= Am;
                break;
            case 99:
                StartCoroutine(Move(Am));
                Controller.gamestate = false;
                break;
            
        }

    }

    IEnumerator Move(int AM)
    {
        int Ori = PlayerPosition - AM;
        for (int i = 0; i < AM; i++)
        {
            PlaceIntoTile(Ori+i+1);
            yield return new WaitForSeconds(0.3f);
        }

        TileEntity destination = Board.TileArray[PlayerPosition].GetComponent<TileEntity>();
        Debug.Log("Enacting Action");
        destination.TileAction(this);
    }

    public void MovePath(Spline temp, int temp2)
    {
        StartCoroutine(MoveSpline(temp, temp2));
    }
    IEnumerator MoveSpline(Spline move,int destitile)
    {
        int speed = Controller.Climbspeed;
        float len = move.CalculateLength(transform.localToWorldMatrix);
        Transform playerpos = gameObject.transform;
        for (float i = 0; i < 101; i++)
        {
            Debug.Log("Applying: "+ i/100);
            playerpos.position = move.EvaluatePosition(i / 100);
            playerpos.position += new Vector3(0, 0, -1);
            Debug.Log(move.EvaluatePosition(i / 100));
            yield return new WaitForSeconds(speed * len / 1000);
        }
        
        PlaceIntoTile(destitile);
    }
}
