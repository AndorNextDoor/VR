using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied = false;


    public void OccupyTile()
    {
        isOccupied = true;
    }
}
