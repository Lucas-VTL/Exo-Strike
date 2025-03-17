using UnityEngine;

public class RotateController : MonoBehaviour
{    
    public float rotateSpeed;

    void Update()
    {
        if (Time.timeScale != 0)
        {
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);    
        }
    }
}
