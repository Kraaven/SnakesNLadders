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
    
    public void initSnake(Vector3 initialPoint, Vector3 finalPoint, int knots)
    {
        Vector3 tempstart = new Vector3(0,0,0);
        Vector3 tempend = new Vector3(Vector3.Distance(initialPoint, finalPoint),0,0);
        path = gameObject.GetComponent<SplineContainer>()[0];
        
        // path.Add(new BezierKnot(tempstart));
        // path.Add(new BezierKnot(tempend));
        for (float i = 0; i < knots+1; i++)
        {
            path.Add(new BezierKnot(Vector3.Lerp(tempstart,tempend,i/knots))); 
        }
        
        for (int i = 1; i < path.Count-1; i++)
        {
            Debug.Log(path.ToArray()[i].Position);
            path.SetKnot(i,new BezierKnot(new float3(path[i].Position.x+Random.Range(-0.5f,0.5f),path[i].Position.y+Random.Range(-1f,1f),0)));
            Debug.Log(path.ToArray()[i].Position + " after");
        }
        
        var all = new SplineRange (0, path.Count); 
        path.SetTangentMode(all, TangentMode.AutoSmooth);
    }
    
}
