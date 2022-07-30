using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotationer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject _templateObject;
    [SerializeField] private float _viscosity;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _pointTouchButton;
    
    public void OnBeginDrag(PointerEventData touch)
    {

    }
    
    public void OnDrag(PointerEventData touch)
    {
        var currentPosition=_camera.WorldToScreenPoint(_pointTouchButton.position);
        var transformPosition = _camera.WorldToScreenPoint(transform.position);
        var targetPosition = touch.position;
        
        var currentDir = (Vector2)transformPosition - (Vector2)currentPosition;
        var targetDir = (Vector2)transformPosition - targetPosition;
        
        transform.Rotate(Vector3.forward,Vector2.SignedAngle(currentDir,targetDir));
        //transform.right = Vector3.Lerp(currentDir, targetDir, 100f * Time.deltaTime);
    }   

    public void OnEndDrag(PointerEventData touch)
    {

    }
}
