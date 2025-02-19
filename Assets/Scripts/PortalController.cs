using UnityEngine;

public class PortalController : MonoBehaviour
{
    private float _maxSpeed = 300f;
    private float _totalTime = 3.5f;
    private float _acceleration;
    private float _elapseTime = 0f;
    
    void Start()
    {
        _acceleration = _maxSpeed / _totalTime;
    }
    
    void Update()
    {
        _elapseTime += Time.deltaTime;

        if (_elapseTime <= _totalTime)
        {
            transform.Rotate(Vector3.forward, _acceleration * _elapseTime * Time.deltaTime);   
        }
        else
        {
            transform.Rotate(Vector3.forward,  _maxSpeed * Time.deltaTime);
        }
    }
}
