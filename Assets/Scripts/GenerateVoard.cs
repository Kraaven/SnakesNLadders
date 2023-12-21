using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateVoard : MonoBehaviour
{
    public GameObject TileBase;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        for (int i = 0; i < 10; i+=2)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject tile = Instantiate(TileBase, new Vector3(pos.x+j, pos.y+i, 0), Quaternion.identity);
                tile.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = (i * 10 + j+1).ToString();
            }

            for (int j = 10; j > 0; j--)
            {
                GameObject tile = Instantiate(TileBase, new Vector3(pos.x+j-1, pos.y+i+1, 0), Quaternion.identity);
                tile.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = (i * 10 + ((10 - j))+11).ToString();
            }
        }
    }
    
}
