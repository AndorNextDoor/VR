using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] public static List<Transform> wayPoints = new List<Transform>();
    public static Path Instance;

    private void Awake()
    {
        Instance = this;

        foreach(Transform t in transform)
        {
            wayPoints.Add(t);
        }
    }
}
