using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotationer : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _turningForce;
    
    private Vector2 _pointTouchPosition;
    public void OnBeginDrag(PointerEventData touch)
    {
        _pointTouchPosition=touch.position;
    }
    
    public void OnDrag(PointerEventData touch)
    {
        RotateObject(touch.position);
    }
    
    private void RotateObject(Vector2 targetPosition)
    {
        var transformPosition = (Vector2)_camera.WorldToScreenPoint(transform.position);

        var currentDir = transformPosition - _pointTouchPosition;
        var targetDir = transformPosition - targetPosition;

        var rectRotation = _rectTransform.rotation.eulerAngles.x;
        var angle = Vector2.SignedAngle(currentDir, targetDir) * Math.Cos(rectRotation * Math.PI / 180.0);

        transform.Rotate(Vector3.forward, (float)angle*_turningForce);
        _pointTouchPosition = targetPosition;
    }
}
