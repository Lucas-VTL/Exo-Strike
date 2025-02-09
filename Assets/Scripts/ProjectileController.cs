using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private int _speed = 20;
    private Vector3 _initialPosition;
    private int _maxDistance = 15;
    
    void Awake()
    {
        _initialPosition = transform.position;
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
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
