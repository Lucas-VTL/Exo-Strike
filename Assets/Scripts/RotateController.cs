using UnityEngine;

public class RotateController : MonoBehaviour
{    
    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
