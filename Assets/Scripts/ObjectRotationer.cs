using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotationer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _pointTouchButton;
    [SerializeField] private RectTransform _rectTransform;
    private Vector2 _pointTouchPosition;
    public void OnBeginDrag(PointerEventData touch)
    {
        _pointTouchPosition=touch.position;
    }
    
    public void OnDrag(PointerEventData touch)
    {
        RotateObject(touch.position);
    }
    
    public void OnEndDrag(PointerEventData touch)
    {

    }
    
    private void RotateObject(Vector2 targetPosition)
    {
        var currentPosition = (Vector2)_camera.WorldToScreenPoint(_pointTouchButton.position);
        var transformPosition = (Vector2)_camera.WorldToScreenPoint(transform.position);

        var currentDir = transformPosition - currentPosition;
        var targetDir = transformPosition - targetPosition;

        var rectRotation = _rectTransform.rotation.eulerAngles.x;
        var angle = Vector2.SignedAngle(currentDir, targetDir) * Math.Cos(rectRotation * Math.PI / 180.0);

        transform.Rotate(Vector3.forward, (float)angle);
    }
}
