using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMouse : MonoBehaviour
{
    private void OnMouseEnter()
    {
        BuildingManager.singleton.resourcePoint = transform;
    }

    private void OnMouseExit()
    {
        BuildingManager.singleton.resourcePoint = null;
    }
}
