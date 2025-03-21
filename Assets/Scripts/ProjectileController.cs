using System;
using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public ProjectileParameter projectileParameter;
    
    private GameObject _returnTarget;
    private bool _isReturning;
    private Vector3 _returnPosition;
    private bool _isReturnable;
    
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
        _currentTime = projectileParameter.existTime;
        _isReturning = false;
        
        if (projectileParameter.speed == 0)
        {
            Destroy(gameObject, projectileParameter.existTime);
        } 
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
         if (_isReturning)
                 {
                     if (_returnTarget)
                     {
                         _returnPosition = _returnTarget.transform.position;
                     }
                     
                     var direction = (_returnPosition - transform.position).normalized;
                     transform.Translate(direction * (projectileParameter.speed *  Time.deltaTime));
                     
                     if (Vector3.Distance(transform.position, _returnPosition) < 0.1f)
                     {
                         Destroy(gameObject);
                     }
                     
                     return;
                 }
                 
                 if (projectileParameter.speed > 0)
                 {
                     transform.Translate(Vector3.right * (projectileParameter.speed *  Time.deltaTime));
                     var distance = Vector3.Distance(_initialPosition, transform.position);
                     if (distance >= projectileParameter.maxDistance)
                     {
                         if (projectileParameter.collisionParticle)
                         {
                             GameObject particle = Instantiate(projectileParameter.collisionParticle, transform.position + new Vector3(_radius * Mathf.Cos(_angle), _radius * Mathf.Sin(_angle), 0), Quaternion.Euler(0,0,0));
                             Destroy(particle, 0.8f);
                         }
                         
                         if (projectileParameter.isExplodeOnEnd)
                         {
                             var effect = Instantiate(projectileParameter.effectOnEnd, transform.position, Quaternion.Euler(0, 0, 0)); 
                             effect.gameObject.GetComponent<ProjectileController>().SetDamage(_damage);  
                         }
         
                         if (_isReturnable)
                         {
                             _isReturning = true;
                             transform.rotation = Quaternion.Euler(0, 0, 0);
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") ||
            (gameObject.CompareTag("Projectile") && other.CompareTag("Monster")) ||
            (gameObject.CompareTag("Monster Projectile") && other.CompareTag("Player")))
        {
            if (projectileParameter.speed > 0)
            {
                if (projectileParameter.collisionParticle)
                {
                    GameObject particle = Instantiate(projectileParameter.collisionParticle, transform.position + new Vector3(_radius * Mathf.Cos(_angle), _radius * Mathf.Sin(_angle), 0), Quaternion.Euler(0,0,0));
                    Destroy(particle, 0.8f);
                }
                
                if (projectileParameter.isExplodeOnEnd)
                {
                    var effect = Instantiate(projectileParameter.effectOnEnd, transform.position, Quaternion.Euler(0, 0, 0)); 
                    effect.gameObject.GetComponent<ProjectileController>().SetDamage(_damage);  
                }
                
                Destroy(gameObject);
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
        _returnPosition = _returnTarget.transform.position;
        _isReturnable = true;
    }
}
