using UnityEngine;

public class AltarController : MonoBehaviour
{
    public float destroyTime;
    private float _rotateSpeed = 30f;
    
    void Start()
    {
        Destroy(gameObject, destroyTime);    
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster") && other.gameObject.GetComponent<MonsterController>().GetGrave() != null)
        {
            other.gameObject.GetComponent<MonsterController>().SetIsReviveable(true);
        }
    }
}
