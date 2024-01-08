using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class InitSnake : MonoBehaviour
{
    public Spline path;
    
    public void initSnake(GameObject T1, GameObject T2, int knots)
    {
        Vector3 initialPoint = T1.transform.position;
        Vector3 finalPoint = T2.transform.position;
         Vector3 tempstart = new Vector3(0,0,0);
         Vector3 tempend = new Vector3(Vector3.Distance(initialPoint, finalPoint),0,0);
         path = gameObject.GetComponent<SplineContainer>()[0];
        
         for (float i = 0; i < knots+1; i++)
         {
             path.Add(new BezierKnot(Vector3.Lerp(tempstart,tempend,i/knots))); 
         }
        
         for (int i = 1; i < path.Count-1; i++)
         {
             path.SetKnot(i,new BezierKnot(new float3(path[i].Position.x+Random.Range(-0.5f,0.5f),path[i].Position.y+Random.Range(-1f,1f),0)));
         }
        
         var all = new SplineRange (0, path.Count); 
         path.SetTangentMode(all, TangentMode.AutoSmooth);

         transform.position = initialPoint;
         Vector3 dir = initialPoint - finalPoint;
         float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
         transform.Rotate(0,0,180);
         
         Debug.Log("Generated Snake from: "+ T1.GetComponent<TileEntity>().TileNum+" -> "+ T2.GetComponent<TileEntity>().TileNum);
        
        // path = gameObject.GetComponent<SplineContainer>()[0];
        // path.Add(new BezierKnot(initialPoint));
        // path.Add(new BezierKnot(finalPoint));
    }
    
    
}
