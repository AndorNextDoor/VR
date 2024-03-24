using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlaceTowerOnCollision : MonoBehaviour
{

    private Transform tower;
    private bool isVacuuming = false;
    [SerializeField] private TowerPlacementController towerPlacementController;
    [SerializeField] private bool IsEmpty = true;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TowerPlacement"))
        {
            tower = other.transform;
            if (!isVacuuming && IsEmpty)
            {
                StartCoroutine(VacuumTowerObject());
            }
        }
    }


    private IEnumerator VacuumTowerObject()
    {
        isVacuuming = true;

        Vector3 startPosition = tower.position;
        Vector3 targetPosition = transform.position; // Center of the tile

        float vacuumDuration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < vacuumDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / vacuumDuration);

            // Interpolate position from start to target
            tower.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        // Ensure the tower reaches the target position exactly
        tower.position = targetPosition;

        isVacuuming = false;

        int index = tower.GetComponent<TowerPlacementObject>().towerIndex;
        Destroy(tower.gameObject);
        towerPlacementController.PlaceTower(index, transform);
        IsEmpty = false;


    }
}
