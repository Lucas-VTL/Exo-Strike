using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Parameter", menuName = "Projectile Parameter")]
public class ProjectileParameter : ScriptableObject
{
    public GameObject collisionParticle;
    public GameObject effectOnEnd;
    public float speed;
    public int maxDistance;
    public bool isExplodeOnEnd;
    public float existTime;
    
    [NonSerialized]
    private GameObject _collisionParticle;
    [NonSerialized]
    private GameObject _effectOnEnd;
    [NonSerialized]
    private float _speed;
    [NonSerialized]
    private int _maxDistance;
    [NonSerialized]
    private bool _isExplodeOnEnd;
    [NonSerialized]
    private float _existTime;

    private void OnEnable()
    {
        _collisionParticle = collisionParticle;
        _effectOnEnd = effectOnEnd;
        _speed = speed;
        _maxDistance = maxDistance;
        _isExplodeOnEnd = isExplodeOnEnd;
        _existTime = existTime;
    }

    private void OnDisable()
    {
        collisionParticle = _collisionParticle;
        effectOnEnd = _effectOnEnd;
        speed = _speed;
        maxDistance = _maxDistance;
        isExplodeOnEnd = _isExplodeOnEnd;
        existTime = _existTime;
    }

    public void ResetData()
    {
        collisionParticle = _collisionParticle;
        effectOnEnd = _effectOnEnd;
        speed = _speed;
        maxDistance = _maxDistance;
        isExplodeOnEnd = _isExplodeOnEnd;
        existTime = _existTime;
    }
}
