using System.Runtime.CompilerServices;
using UnityEngine;

public class PortalSpawning : MonoBehaviour
{
    public GameObject moveScope;
    public GameObject waterScope;
    public GameObject portal;
    
    private BoxCollider2D _moveScopeCollider;
    private PolygonCollider2D[] _waterScopeCollider;
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
        
        _waterScopeCollider = waterScope.GetComponents<PolygonCollider2D>();
        
        InvokeRepeating("SpawnPortal", 0f, 5f);
    }

    void SpawnPortal()
    {
        bool isValid = false;
        Vector3 position = new Vector3();

        while (!isValid)
        {
            var randomX = Random.Range(-_xBoundary + _xBoundaryOffset, _xBoundary - _xBoundaryOffset);
            var randomY = Random.Range(-_yBoundary + _yBottomBoundaryOffset, _yBoundary + _yTopBoundaryOffset);
            
            position.x = randomX;
            position.y = randomY;
            position.z = 0;

            if (!IsInWaterZone(position))
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
        }
        
        Instantiate(portal, position, Quaternion.Euler(0, 0, 0));
    }

    bool IsInWaterZone(Vector3 point)
    {
        foreach (var zone in _waterScopeCollider)
        {
            if (zone.OverlapPoint(point))
            {
                return true;
            }
        }
        
        return false;
    }
}
