using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GenerateVoard Board;
    public int PlayerID;
    public int PlayerPosition;

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
        Debug.Log("Target Tile: "+ tile.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text);

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
        Debug.Log("Player "+ PlayerID + " Has been moved to "+ (Id+1)+"("+PlayerPosition+")");
        
    }

    public void AddPosition(int Am)
    {
        Debug.Log("Player "+ PlayerID+ " Has rolled: "+ Am + " And is moving from "+ PlayerPosition);
        PlayerPosition += Am;
        Debug.Log(PlayerPosition-Am + " + "+ Am+ " = "+ PlayerPosition);
        PlaceIntoTile(PlayerPosition);
    }
}
