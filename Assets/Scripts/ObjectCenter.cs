using UnityEngine;

public class ObjectCenter : MonoBehaviour
{
    [SerializeField] private int _objectLevel;
    
    public int ObjectLevel => _objectLevel;
    
    public void SetCenterObject()
    {
        transform.position =  GetComponent<Renderer>().bounds.center;
        transform.localPosition = new Vector3(-transform.localPosition.x,-transform.localPosition.y, 0);
    }
}
