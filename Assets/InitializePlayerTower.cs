using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayerTower : MonoBehaviour
{
    [SerializeField] private int equippedTowerIndex;

    private void Start()
    {
        TowerManager.Instance.EquipNewTower(equippedTowerIndex);
    }
}
