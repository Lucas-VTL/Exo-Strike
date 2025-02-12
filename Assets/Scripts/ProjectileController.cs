using System;
using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject collisionParticle;
    
    private int _speed = 20;
    private Vector3 _initialPosition;
    private int _maxDistance = 15;
    private float _xParticelOffset = 0.75f;
    private float _yParticelOffset = 0f;
    private float _radius;
    private float _angle;
    
    void Awake()
    {
        _initialPosition = transform.position;
        _radius = Mathf.Sqrt(_xParticelOffset * _xParticelOffset + _yParticelOffset * _yParticelOffset);
        _angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
    }

    void Update()
    {
        transform.Translate(Vector3.right * (_speed *  Time.deltaTime));
        var distance = Vector3.Distance(_initialPosition, transform.position);
        if (distance >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Monster"))
        {
            Destroy(gameObject);
            GameObject particle = Instantiate(collisionParticle, transform.position + new Vector3(_radius * Mathf.Cos(_angle), _radius * Mathf.Sin(_angle), 0), Quaternion.Euler(0,0,0));
            Destroy(particle, 0.8f);
        }
    }
}
