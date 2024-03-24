using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private UiPlaceTowerButtons placeTowerButtonsManager;
    [SerializeField] private UiShowTowersToSelect uiShowTowersToSelect;
    public static TowerManager Instance;
    
    public List<XRSocketInteractor> TowerObjects = new List<XRSocketInteractor>();

    public List<TowerData> TowerList = new List<TowerData>();


    private void Awake()
    {
        Instance = this;   
    }

    private void Start()
    {
        
    }

    public void ShowRandomTowers()
    {
        if (!CanEquipMoreTowers()) return; // Cant equip more towers add ui for that or disable button to get towers and only show the number of perks
        AdditionalTools.Shuffle(TowerList);
        uiShowTowersToSelect.ResetButtonsList();
        uiShowTowersToSelect.ShowHideTowerSelectionMenu(true);
        GameManager.Instance.DecreaseSkillpoints();
    }


    public bool CanEquipMoreTowers()
    {
        int i = 0;

        foreach (var tower in TowerList)
        {
            if (tower.IsEquipped)
            {
                i++;
            }
        }
        if (i >= 5)
        {
            return false;
        }
        else return true;
    }

    public void EquipNewTower(int index)
    {
        TowerData tower = TowerList.Find(t => t.towerIndex == index);

        if (tower != null)
        {
            tower.IsEquipped = true;
        }


        ResetTowers();
        //placeTowerButtonsManager.ResetButtonsList();
        //  uiShowTowersToSelect.ShowHideTowerSelectionMenu(false);
    }

    public void ResetTowers()
    {
        StartCoroutine(ResetTowersCoroutine());
    }

    public void OnTowerLost(int _towerIndex)
    {
        TowerData tower = TowerList.Find(t => t.towerIndex == _towerIndex);

        if (tower != null)
        {
            tower.IsInInventory = false;
        }

    }

    IEnumerator ResetTowersCoroutine()
    {
        yield return new WaitForSeconds(1);

        GameObject[] placementTowers = GameObject.FindGameObjectsWithTag("TowerPlacement");

        foreach (var placement in placementTowers)
        {
            Destroy(placement.gameObject);
        }

        foreach (TowerData towerData in TowerList)
        {
            if (!towerData.IsEquipped) continue;
            if (towerData.IsPlaced) continue;
            

            Tower tower = towerData.towerPrefab.transform.GetChild(0).GetComponent<Tower>();
            XRSocketInteractor towerObject = FindOpenTowerPlacementSlot();
            if (towerObject == null)
            {
                break;
            }
            Transform newTowerSlot = Instantiate(tower.TowerPlacementPrefab, towerObject.transform.position, tower.TowerPlacementPrefab.transform.rotation).transform;
            newTowerSlot.transform.position = towerObject.transform.position;
            towerData.IsInInventory = true;
            yield return new WaitForSeconds(0.3f);
        }
    }

    public XRSocketInteractor FindOpenTowerPlacementSlot()
    {
        foreach (XRSocketInteractor towerObject in TowerObjects)
        {
            if (towerObject.interactablesSelected.Count == 0)
            {
                return towerObject;
            }
        }
        return null;
    }

}

[Serializable]
public class TowerData
{
    public GameObject towerPrefab;
    public int towerIndex;
    public bool IsPlaced;
    public bool IsEquipped;
    public bool IsInInventory;
}
