using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPlaceTowerButtons : MonoBehaviour
{
    [SerializeField] private Transform buttonsHolder;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private TowerManager towerManager;
    [SerializeField] private TowerPlacementController towerPlacementController;


    private void Start()
    {
        ClearButtonsList();
        CreateButtonsList();
    }


    public void ResetButtonsList()
    {
        ClearButtonsList();
        CreateButtonsList();
    }

    public void CreateButtonsList()
    {
        foreach (TowerData towerData in towerManager.TowerList)
        {
            if (!towerData.IsEquipped) continue;

            Tower tower = towerData.towerPrefab.transform.GetChild(0).GetComponent<Tower>();
            Transform newButton = Instantiate(buttonPrefab, buttonsHolder).transform;
            //Change the variables of button
            newButton.GetChild(0).GetComponent<Image>().sprite = tower.towerSprite;
            newButton.GetChild(1).GetComponent<TextMeshProUGUI>().text = tower.towerCost.ToString();
        }
    }

    public void ClearButtonsList()
    {
        foreach (Transform child in buttonsHolder)
        {
            Destroy(child.gameObject);
        }
    }

}
