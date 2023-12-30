using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateLadder : MonoBehaviour
{
    public GameObject TopTile;
    public GameObject MidTile;
    public GameObject BottomTile;

    [Header("Targets")] 
    public GameObject Target1;
    public GameObject Target2;
    
    
    public void InitLadder(GameObject T1, GameObject T2)
    {
        Target1 = T1;
        Target2 = T2;
        int Dfac = (int)Vector2.Distance(Target1.transform.position, Target2.transform.position);
        if (Dfac % 2 == 0)
        {
            float st = (Dfac / 2) + 0.5f;
            Debug.Log("TopTile: " + st);
            GameObject top = Instantiate(TopTile, new Vector2(0,st), Quaternion.Euler(0,0,0));
            top.transform.parent = gameObject.transform;
            st--;
            for(float i=st;i>-Dfac/2;i--)
            {
                GameObject mid = Instantiate(MidTile, new Vector2(0,i), Quaternion.Euler(0,0,0));
                mid.transform.parent = gameObject.transform;
                st = i;
            }

            st--;
            
            GameObject bottom = Instantiate(BottomTile, new Vector2(0,st), Quaternion.Euler(0,0,0));
            bottom.transform.parent = gameObject.transform;

        }
        else
        {
            float st = (Dfac + 1) / 2;
            GameObject top = Instantiate(TopTile, new Vector2(0,st), Quaternion.Euler(0,0,0));
            top.transform.parent = gameObject.transform;
            st--;
            for(float i=st;i>(-Dfac/2)-1;i--)
            {
                GameObject mid = Instantiate(MidTile, new Vector2(0,i), Quaternion.Euler(0,0,0));
                mid.transform.parent = gameObject.transform;
                st = i;
            }

            st--;
            GameObject bottom = Instantiate(BottomTile, new Vector2(0,st), Quaternion.Euler(0,0,0));
            bottom.transform.parent = gameObject.transform;
        }
        
        
        transform.position = Vector2.Lerp(Target1.transform.position, Target2.transform.position,0.5f);
        transform.Translate(0,0,-1.5f);
        transform.localScale = new Vector3(0.60f, 1, 1);
        LookAt2D();


    }
    

    private void LookAt2D()
    {
        Vector3 dir = Target1.transform.position - Target2.transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.Rotate(0,0,90);
    }
}
