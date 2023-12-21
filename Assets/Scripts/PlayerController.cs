using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Board;
    public int PlayerID;

    public void initPlayer(int pos)
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
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
