using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiShowTowersToSelect : MonoBehaviour
{
    [SerializeField] private Transform buttonsHolder;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private TowerManager towerManager;
    [SerializeField] private GameObject selectMenu;


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

    public void ShowHideTowerSelectionMenu(bool show)
    {
        selectMenu.SetActive(show);
    }

    public void CreateButtonsList()
    {
        int buttonCount = 0; // Counter to track the number of buttons created

        foreach (TowerData towerData in towerManager.TowerList)
        {
            if (buttonCount >= 3) break; // Exit the loop once 3 buttons are created

            if (towerData.IsEquipped) continue;

            Tower tower = towerData.towerPrefab.transform.GetChild(0).GetComponent<Tower>();
            Transform newButton = Instantiate(buttonPrefab, buttonsHolder).transform;
            //Change the variables of button
            newButton.GetComponent<Button>().image.sprite = towerData.towerPrefab.transform.GetChild(0).GetComponent<Tower>().towerSprite;
            TowerChooseButton towerButtonOnClick = newButton.GetComponent<TowerChooseButton>();
            towerButtonOnClick.towerIndex = towerData.towerIndex;

            buttonCount++; // Increment the button count
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
