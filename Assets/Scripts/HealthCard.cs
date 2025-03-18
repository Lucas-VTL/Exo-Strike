using UnityEngine;

public class HealthCard : Card
{
    private float _buffParameter;

    public HealthCard(float buffParameter, GameObject target, int level, string description, string type, Sprite cardImage, Sprite targetImage)
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
                if (Type == "Heal")
                {
                    Target.gameObject.GetComponent<PlayerController>().AddHealth((int)_buffParameter);
                }

                if (Type == "Increase")
                {
                    Target.gameObject.GetComponent<PlayerController>().IncreaseHealth((int)_buffParameter);
                }

                if (Type == "Invulnerable")
                {
                    Target.gameObject.GetComponent<PlayerController>().AddInvulenerableTime(_buffParameter);
                }
            }
            else
            {
                if (Target.tag == "Monster")
                {
                    if (Type == "Increase")
                    {
                        Target.gameObject.GetComponent<MonsterController>().monsterParameter.health += (int)_buffParameter;
                    }
                }
            }
        }
    }
}
