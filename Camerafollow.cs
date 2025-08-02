using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    public Transform carTransform;
    void LateUpdate()
    {
        transform.position = new Vector3(carTransform.position.x,carTransform.position.y,-10f);

    }
}