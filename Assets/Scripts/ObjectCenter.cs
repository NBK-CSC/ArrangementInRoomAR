using System;
using UnityEngine;

public class ObjectCenter : MonoBehaviour
{
    public void SetCenterObject()
    {
        transform.position =  GetComponent<Renderer>().bounds.center;
        transform.localPosition = new Vector3(-transform.localPosition.x,-transform.localPosition.y, 0);
    }
}
