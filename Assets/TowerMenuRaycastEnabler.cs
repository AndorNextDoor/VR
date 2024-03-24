using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenuRaycastEnabler : MonoBehaviour
{
    [SerializeField] private Transform leftWrist;
    [SerializeField] private GameObject menu;
    [SerializeField] private LayerMask rightLayer;

    private bool wasEnabled = false;

    public Action OnInventoryEnable;
    public Action OnInventoryDisable;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(leftWrist.position, (leftWrist.forward) * 50, out hit,20, rightLayer))
        {
            if (hit.collider.CompareTag("PlayerFace"))
            {

                menu.SetActive(true);
                if (!wasEnabled)
                {
                    OnInventoryEnable.Invoke();
                }
                wasEnabled = true;

            }
            else
            {
                menu.SetActive(false);
                if (wasEnabled)
                {
                    OnInventoryDisable.Invoke();
                }
                wasEnabled = false;
            }
        }
        else
        {
            menu.SetActive(false);
            if (wasEnabled)
            {
                OnInventoryDisable.Invoke();
            }
            wasEnabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftWrist.position, (leftWrist.forward) * 50);
    }
}
