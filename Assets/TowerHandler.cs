using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private int maxTowers = 6;
    [SerializeField] private GameObject towerPrefab; // Reference to the tower prefab

    private void Start()
    {
        // Ensure we have the correct number of towers
        UpdateTowerPositions();
    }

    // Method to update tower positions
    private void UpdateTowerPositions()
    {
        int numTowers = Mathf.Min(transform.childCount, maxTowers);
        float angleIncrement = 360f / numTowers;

        for (int i = 0; i < numTowers; i++)
        {
            float angle = i * angleIncrement;
            Vector3 positionOffset = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;

            Transform tower = transform.GetChild(i);
            tower.localPosition = positionOffset;
        }
    }

    // Method to add a tower to the handler
    public void AddTower()
    {
        if (transform.childCount < maxTowers && towerPrefab != null)
        {
            Instantiate(towerPrefab, transform);
            UpdateTowerPositions();
        }
    }

    // Method to remove a tower from the handler
    public void RemoveTower()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            UpdateTowerPositions();
        }
    }
}
