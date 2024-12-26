using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCollider : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _polygonCollider2D;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    
    void Update()
    {
        if (_polygonCollider2D&& _spriteRenderer)
        {
            _polygonCollider2D.pathCount = _spriteRenderer.sprite.GetPhysicsShapeCount();
            for (int i = 0; i < _polygonCollider2D.pathCount; i++)
            {
                List<Vector2> shape = new List<Vector2>(_spriteRenderer.sprite.GetPhysicsShape(i, new List<Vector2>()));
                _spriteRenderer.sprite.GetPhysicsShape(i, shape);
                _polygonCollider2D.SetPath(i, shape);
            }
        }
    }
}
