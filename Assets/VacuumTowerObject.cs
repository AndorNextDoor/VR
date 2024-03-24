using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumTowerObject : MonoBehaviour
{
    public bool IsTowerEquipped;
    private Transform tower;
    private bool isVacuuming = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TowerPlacement"))
        {
            tower = other.transform;
            if (!isVacuuming)
            {
                StartCoroutine(VacuumTower());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TowerPlacement"))
        {
            tower = null;
            IsTowerEquipped = false;
        }
    }

    private IEnumerator VacuumTower()
    {
        
        tower.GetComponent<MoveToObjectPosition>().SetNewFollowTransform(transform);

        
        isVacuuming = true;

        Vector3 startPosition = tower.position;

        float vacuumDuration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < vacuumDuration)
        {
            if (tower == null) yield break;

            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / vacuumDuration);

            // Interpolate position from start to target
            tower.position = Vector3.Lerp(startPosition, transform.position, t);

            yield return null;
        }

        // Ensure the tower reaches the target position exactly
        tower.position = transform.position;

        isVacuuming = false;
    }

}
