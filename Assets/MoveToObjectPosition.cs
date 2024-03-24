using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObjectPosition : MonoBehaviour
{
    [SerializeField] private Transform ToFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Quaternion rotationOffset;

    void Update()
    {
        if(ToFollow == null) return;
        transform.position = ToFollow.position + offset;
        transform.rotation = ToFollow.rotation * rotationOffset;
    }

    public void SetNewFollowTransform(Transform _transform)
    {
        ToFollow = _transform;
    }
}
