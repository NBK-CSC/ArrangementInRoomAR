using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private Transform _objectPlace;
    [SerializeField] private Transform _objectRotation;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _container;
    [SerializeField] private float _minAngleInclination;
    
    private ARRaycastManager _arRaycastManager;
    private GameObject _installedObject;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private void Start()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _objectRotation.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdatePlacementPose();   
        if (Input.touchCount==2)
            SetObject();
    }

    private void UpdatePlacementPose()
    {
        Vector2 screenCenter = _camera.ViewportToScreenPoint(new Vector2(0.5f,0.5f));
        var ray = _camera.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            SetObjectPosition(raycastHit.point, raycastHit.normal);
        }
        else if (_arRaycastManager.Raycast(screenCenter, _hits, TrackableType.PlaneWithinPolygon))
        {
            SetObjectPosition(_hits[0].pose.position, Vector3.zero);
        }
    }

    public void SetObject()
    {
        _installedObject.GetComponent<Collider>().enabled = true;
        _installedObject.transform.parent = _container.transform;
        _installedObject=null;
        _objectRotation.gameObject.SetActive(false);
    }
    
    private void SetObjectPosition(Vector3 point, Vector3 normal)
    {
        _objectPlace.position = point;
        _objectPlace.forward=-normal;
    }

    public void SetInstallObject(ItemData itemData)
    {
        if (_installedObject != null)
            Destroy(_installedObject);
        _objectRotation.gameObject.SetActive(true);
        _installedObject = Instantiate(itemData.Prefab, _objectRotation);
        _installedObject.GetComponent<ObjectCenter>().SetCenterObject();
        _installedObject.GetComponent<Collider>().enabled = false;
    }
}
