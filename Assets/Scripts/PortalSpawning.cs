using UnityEngine;

public class PortalSpawning : MonoBehaviour
{
    public GameObject moveScope;
    public GameObject portal;
    
    private BoxCollider2D _moveScopeCollider;
    private float _xBoundary;
    private float _yBoundary;
    private float _xBoundaryOffset = 4f;
    private float _yTopBoundaryOffset = 1.5f;
    private float _yBottomBoundaryOffset = 4f;
    
    void Start()
    {
        _moveScopeCollider = moveScope.GetComponent<BoxCollider2D>();
        _xBoundary = _moveScopeCollider.size.x / 2;
        _yBoundary = _moveScopeCollider.size.y / 2;
        
        InvokeRepeating("SpawnPortal", 0f, 2f);
    }
    
    void Update()
    {

    }

    void SpawnPortal()
    {
        var randomX = Random.Range(-_xBoundary + _xBoundaryOffset, _xBoundary - _xBoundaryOffset);
        var randomY = Random.Range(-_yBoundary + _yBottomBoundaryOffset, _yBoundary + _yTopBoundaryOffset);
        var position = new Vector3(randomX, randomY, 0);
        Instantiate(portal, position, Quaternion.Euler(0, 0, 0));
    }
}
