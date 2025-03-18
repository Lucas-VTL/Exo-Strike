using UnityEngine;

public class EnergyCard : Card
{
    private float _buffParameter;

    public EnergyCard(float buffParameter, GameObject target, int level, string description, string type, Sprite cardImage, Sprite targetImage)
    {
        _buffParameter = buffParameter;
        Target = target;
        Level = level;
        Description = description;
        Type = type;
        CardImage = cardImage;
        TargetImage = targetImage;
    }
    
    public override void ApplyBuff()
    {
        if (Target != null)
        {
            if (Target.tag == "Player")
            {
                if (Type == "Increase")
                {
                    Target.gameObject.GetComponent<PlayerController>().IncreaseStamina((int)_buffParameter);
                }

                if (Type == "Cooldown")
                {
                    Target.gameObject.GetComponent<PlayerController>().DecreaseStaminaCooldownMax(_buffParameter);
                }
            }
            else
            {
                if (Type == "Cooldown")
                {
                    Target.gameObject.GetComponent<MonsterController>().monsterParameter.attackCooldown *= (1 - _buffParameter);
                }
            }
        }
    }

    public float GetBuffParameter()
    {
        return _buffParameter;
    }
}
