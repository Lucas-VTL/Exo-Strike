using System;
using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject collisionParticle;
    private int _damage;
    
    public int speed;
    private Vector3 _initialPosition;
    public int maxDistance;
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
        transform.Translate(Vector3.right * (speed *  Time.deltaTime));
        var distance = Vector3.Distance(_initialPosition, transform.position);
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") ||
            (gameObject.CompareTag("Projectile") && other.CompareTag("Monster")) ||
            (gameObject.CompareTag("Monster Projectile") && other.CompareTag("Player")))
        {
            Destroy(gameObject);
            GameObject particle = Instantiate(collisionParticle, transform.position + new Vector3(_radius * Mathf.Cos(_angle), _radius * Mathf.Sin(_angle), 0), Quaternion.Euler(0,0,0));
            Destroy(particle, 0.8f);
        }
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public int GetDamage()
    {
        return _damage;
    }
}
