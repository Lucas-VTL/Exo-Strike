using UnityEngine;

public class OnEndEffectController : MonoBehaviour
{
    private int _damage;

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public int GetDamage()
    {
        return _damage;
    }
}
