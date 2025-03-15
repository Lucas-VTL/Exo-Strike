using System;
using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject collisionParticle;
    public GameObject effectOnEnd;
    public int speed;
    public int maxDistance;
    public bool isExplodeOnEnd;
    public float existTime;
    
    private GameObject _returnTarget;
    private bool _isReturning;
    private int _damage;
    private Vector3 _initialPosition;
    private float _xParticelOffset = 0.75f;
    private float _yParticelOffset = 0f;
    private float _radius;
    private float _angle;
    private float _currentTime;
    
    void Awake()
    {
        _initialPosition = transform.position;
        _radius = Mathf.Sqrt(_xParticelOffset * _xParticelOffset + _yParticelOffset * _yParticelOffset);
        _angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        _currentTime = existTime;
        _isReturning = false;

        if (speed == 0)
        {
            Destroy(gameObject, existTime);
        } 
    }

    void Update()
    {
        if (_isReturning)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
            var direction = (_returnTarget.transform.position - transform.position).normalized;
            transform.Translate(direction * (speed *  Time.deltaTime));

            if (Vector3.Distance(transform.position, _returnTarget.transform.position) < 0.1f)
            {
                Destroy(gameObject);
            }
            
            return;
        }
        
        if (speed > 0)
        {
            transform.Translate(Vector3.right * (speed *  Time.deltaTime));
            var distance = Vector3.Distance(_initialPosition, transform.position);
            if (distance >= maxDistance)
            {
                if (isExplodeOnEnd)
                {
                    var effect = Instantiate(effectOnEnd, transform.position, Quaternion.Euler(0, 0, 0));
                    effect.gameObject.GetComponent<ProjectileController>().SetDamage(_damage);
                }

                if (_returnTarget)
                {
                    _isReturning = true;
                }
                else
                {
                    Destroy(gameObject);
                }
            }   
        }
        else
        {
            _currentTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") ||
            (gameObject.CompareTag("Projectile") && other.CompareTag("Monster")) ||
            (gameObject.CompareTag("Monster Projectile") && other.CompareTag("Player")))
        {
            if (speed > 0)
            {
                Destroy(gameObject);
                
                if (collisionParticle)
                {
                    GameObject particle = Instantiate(collisionParticle, transform.position + new Vector3(_radius * Mathf.Cos(_angle), _radius * Mathf.Sin(_angle), 0), Quaternion.Euler(0,0,0));
                    Destroy(particle, 0.8f);
                }
                
                if (isExplodeOnEnd)
                {
                    var effect = Instantiate(effectOnEnd, transform.position, Quaternion.Euler(0, 0, 0)); 
                    effect.gameObject.GetComponent<ProjectileController>().SetDamage(_damage);  
                }   
            }
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

    public float GetCurrentTime()
    {
        return _currentTime;
    }

    public void SetReturnTarget(GameObject target)
    {
        _returnTarget = target;
    }
}
