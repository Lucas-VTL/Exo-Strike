using UnityEngine;

public class PortalController : MonoBehaviour
{
    private float _maxSpeed = 360f;
    private float _totalSpinningTime = 3.5f;
    private float _elapseSpinningTime = 0f;
    
    private float _destroyTime = 6;
    
    private float _totalDownScaleTime;
    private float _elapseDownScaleTime = 0f;
    
    void Start()
    {
        _totalDownScaleTime = _destroyTime - _totalSpinningTime;
        Destroy(gameObject, _destroyTime);
    }
    
    void Update()
    {
        _elapseSpinningTime += Time.deltaTime;
        var spinSpeed = Mathf.Lerp(0f, _maxSpeed, _elapseSpinningTime / _totalSpinningTime);
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        
        if (_elapseSpinningTime >= _totalSpinningTime)
        {
            _elapseDownScaleTime += Time.deltaTime;
            var downScaleValue = Mathf.Lerp(1f, 0f, _elapseDownScaleTime / _totalDownScaleTime);
            transform.localScale =  new Vector3(downScaleValue, downScaleValue, downScaleValue);
        }
    }
}
