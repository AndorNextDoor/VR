using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRTowerPlacementButton : MonoBehaviour
{

    [SerializeField] private Transform towerToActivate;
    XRGrabInteractable grabInteractable;


    public void OnInteractionWithButton()
    {
        towerToActivate.gameObject.SetActive(true);
        towerToActivate.transform.parent = null;
    }

}
