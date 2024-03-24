using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlacementController : MonoBehaviour
{
    [SerializeField] private TowerManager towerManager;
    [SerializeField] private UiPlaceTowerButtons towerButtonsManager;

    public LayerMask placementLayer;
    public int towerIndex = 0;

    private bool isTowerSelected;
    private Tile currentTile;



    public void PlaceTower(int towerIndex, Transform newPosition)
    {
        if (!towerManager.TowerList[towerIndex].IsPlaced)
        {
            GameObject newTower = Instantiate(towerManager.TowerList[towerIndex].towerPrefab, newPosition.transform.position, towerManager.TowerList[towerIndex].towerPrefab.transform.rotation);
            
            newTower.transform.position = newPosition.transform.position;
            newTower.transform.rotation = towerManager.TowerList[towerIndex].towerPrefab.transform.rotation;

            currentTile = newPosition.GetComponent<Tile>(); 
            currentTile.OccupyTile();
            isTowerSelected = false;

            towerManager.TowerList[towerIndex].IsPlaced = true;
        }
        else
        {
            isTowerSelected = false;
        }
    }

}
