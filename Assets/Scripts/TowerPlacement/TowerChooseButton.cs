using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerChooseButton : MonoBehaviour, IPointerDownHandler
{
    public int towerIndex;


    public void OnPointerDown(PointerEventData data)
    {
        TowerManager.Instance.EquipNewTower(towerIndex);
    }
}
