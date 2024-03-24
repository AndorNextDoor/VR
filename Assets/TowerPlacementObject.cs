using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TowerPlacementObject : MonoBehaviour
{
    public int towerIndex;
    private Rigidbody rb;

    private bool InHands = false;
    private float lostTowerTimer = 10f;

    [SerializeField] XRGrabInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();

        interactable.selectEntered.AddListener(OnSelected);
        interactable.selectExited.AddListener(OnSelectedExit);
    }

    private void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnSelected);
        interactable.selectExited.RemoveListener(OnSelectedExit);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        InHands = true;
    }
    private void OnSelectedExit(SelectExitEventArgs args)
    {
        InHands = false;
    }

    public void OnExitSelection()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        transform.parent = null;
    }

    private void Update()
    {
        if(!InHands && gameObject.layer == 6)
        {
            lostTowerTimer -= Time.deltaTime;

            if(lostTowerTimer <= 0)
            {
                TowerManager.Instance.ResetTowers();
                TowerManager.Instance.OnTowerLost(towerIndex);
                Destroy(gameObject);
            }

        }
    }
}
