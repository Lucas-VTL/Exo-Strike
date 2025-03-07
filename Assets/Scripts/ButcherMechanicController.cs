using UnityEngine;

public class ButcherMechanicController : MonoBehaviour
{
    private Component _monsterController;
    
    void Start()
    {
        _monsterController = this.gameObject.GetComponent<MonsterController>();
    }
    
    void Update()
    {
        
    }
}
