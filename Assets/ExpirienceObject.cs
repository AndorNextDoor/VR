using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpirienceObject : MonoBehaviour
{
    [SerializeField] private int currencyAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GameManager.Instance.GetExperience(currencyAmount);
            Destroy(gameObject);
        }
    }

}
