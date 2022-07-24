using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private Transform _objectPlace;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _container;

    private ARRaycastManager _arRaycastManager;
    private GameObject _installedObject;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private void Start()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
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
            SetObjectPosition(raycastHit.point);
        }
        else if (_arRaycastManager.Raycast(screenCenter, _hits, TrackableType.PlaneWithinPolygon))
        {
            SetObjectPosition(_hits[0].pose.position);
        }
    }

    private void SetObject()
    {
        _installedObject.GetComponent<Collider>().enabled = true;
        _installedObject.transform.parent = _container.transform;
        _objectPlace=null;
    }

    private void SetObjectPosition(Vector3 position )
    {
        _objectPlace.position = position;
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRotation = new Vector3(cameraForward.x, 0, cameraForward.z);
        _objectPlace.rotation = Quaternion.Euler(cameraRotation);
    }

    public void SetInstallObject(ItemData itemData)
    {
        if(_installedObject!=null)
            Destroy(_installedObject);
        _installedObject = Instantiate(itemData.Prefab, _objectPlace);
        _installedObject.GetComponent<Collider>().enabled = false;
    }
}
