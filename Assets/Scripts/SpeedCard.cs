using UnityEngine;

public class SpeedCard : Card
{
    private float _buffParameter;

    public SpeedCard(float buffParameter, GameObject target, int level, string description, string type, Sprite cardImage, Sprite targetImage)
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
                if (Type == "Walk")
                {
                    Target.gameObject.GetComponent<PlayerController>().AddWalkSpeed(_buffParameter);
                }
                
                if (Type == "Run")
                {
                    Target.gameObject.GetComponent<PlayerController>().AddRunSpeed(_buffParameter);
                }
            }
            else
            {
                if (Type == "Walk")
                {
                    Target.gameObject.GetComponent<MonsterController>().monsterParameter.speed += _buffParameter;
                }
            }
        }
    }
}
