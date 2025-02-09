using UnityEngine;

public class GunHeadLightController : MonoBehaviour
{
    private float _existTime = 0.25f;
    
    void Start()
    {
        Destroy(gameObject, _existTime);
    }
}
