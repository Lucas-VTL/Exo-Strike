using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private int _speed = 20;
    
    void Update()
    {
        transform.Translate(Vector3.right * (_speed *  Time.deltaTime));
    }
}
