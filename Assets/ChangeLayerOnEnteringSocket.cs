using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeLayerOnEnteringSocket : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor interactor;
    [SerializeField] private int layerFrom;
    [SerializeField] private int layerTo;
    private GameObject currentObject;
    public TowerMenuRaycastEnabler menuEnabler;

    private bool IsMenuDisabled;

    private void Start()
    {
        interactor.selectEntered.AddListener(ChangeLayerTo);
        interactor.selectExited.AddListener(ChangeLayerFrom);

        menuEnabler.OnInventoryEnable += EnableAttachedTowers;
        menuEnabler.OnInventoryDisable += DisableAttachedTowers;
    }

    public void EnableAttachedTowers()
    {
        if (currentObject == null) return;
        currentObject.SetActive(true);
        currentObject.transform.position = transform.position;
        IsMenuDisabled = false;
    }

    public void DisableAttachedTowers()
    {
        if (currentObject == null) return;
        currentObject.SetActive(false);
        IsMenuDisabled = true;
    }



    private void ChangeLayerTo(SelectEnterEventArgs args)
    {
        currentObject = interactor.interactablesSelected[0].transform.gameObject;
        currentObject.layer = layerTo;
           
    }
    private void ChangeLayerFrom(SelectExitEventArgs args)
    {
        currentObject.layer = layerFrom;
        if (IsMenuDisabled)
        {
            currentObject = null;
        }
    }
}
