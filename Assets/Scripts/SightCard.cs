using Unity.Cinemachine;
using Unity.Cinemachine.Editor;
using UnityEngine;

public class SightCard : Card
{
    private float _buffParameter;

    public SightCard(float buffParameter, GameObject target, int level, string description, string type, Sprite cardImage, Sprite targetImage)
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
            if (Type == "Increase")
            {
                Target.gameObject.GetComponent<CinemachineCamera>().Lens.OrthographicSize += _buffParameter;
                Target.gameObject.GetComponent<CinemachineConfiner2D>().InvalidateLensCache();
            }
        }
    }
}
