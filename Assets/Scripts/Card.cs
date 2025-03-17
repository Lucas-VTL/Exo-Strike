using UnityEngine;

public abstract class Card
{
    public GameObject Target { get; protected set; }
    public int Level { get; protected set; }
    public string Description { get; protected set; }
    public string Type { get; protected set; }
    public Sprite CardImage { get; protected set; }
    public Sprite TargetImage { get; protected set; }

    public abstract void ApplyBuff();
}
