using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class TileEntity : MonoBehaviour
{
    enum TileState
    {
        None,
        Ladder,
        Snake,
    }

    [SerializeField] private TileState state;
    public void Start()
    {

        state = new TileState();
    }
}
