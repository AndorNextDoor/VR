using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private GameObject[] lootVariants;

    public static LootManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnLoot(Vector3 positionToSpawn)
    {
        Instantiate(lootVariants[0], positionToSpawn, Quaternion.identity);
    }
}
