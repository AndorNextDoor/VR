using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTools : MonoBehaviour
{
    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int randomIndex = UnityEngine.Random.Range(0, n + 1);
            T value = list[randomIndex];
            list[randomIndex] = list[n];
            list[n] = value;
        }
    }
}
