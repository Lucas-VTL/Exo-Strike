using UnityEngine;

public class BulletCard : Card
{
    private float _buffParameter;
    private int _indexBullet;
    
    public BulletCard(float buffParameter, GameObject target, int level, string description, string type, Sprite cardImage, Sprite targetImage)
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
                if (Type == "Speed")
                {
                    Target.gameObject.GetComponent<PlayerController>().AddBulletSpeed(_indexBullet, _buffParameter);
                }

                if (Type == "Distance")
                {
                    Target.gameObject.GetComponent<PlayerController>().AddBulletDistance(_indexBullet, (int)_buffParameter);
                }

                if (Type == "Storage")
                {
                    Target.gameObject.GetComponent<PlayerController>().AddBulletStorage(_indexBullet, (int)_buffParameter);
                }

                if (Type == "Cooldown")
                {
                    Target.gameObject.GetComponent<PlayerController>().DecreaseBulletReloadTime(_indexBullet, _buffParameter);
                }

                if (Type == "Damage")
                {
                    Target.gameObject.GetComponent<PlayerController>().IncreaseBulletDamage(_indexBullet, (int)_buffParameter);
                }

                if (Type == "Effect")
                {
                    Target.gameObject.GetComponent<PlayerController>().IncreaseBulletEffect(_indexBullet, _buffParameter);
                }
            }
            else
            {
                if (Target.tag == "Monster")
                {
                    if (Type == "Speed")
                    {
                        Target.gameObject.GetComponent<MonsterController>().monsterParameter.monsterProjectile.gameObject.GetComponent<ProjectileController>().projectileParameter.speed += _buffParameter;
                    }

                    if (Type == "Distance")
                    {
                        Target.gameObject.GetComponent<MonsterController>().monsterParameter.monsterProjectile.gameObject.GetComponent<ProjectileController>().projectileParameter.maxDistance += (int)_buffParameter;
                    }

                    if (Type == "Damage")
                    {
                        Target.gameObject.GetComponent<MonsterController>().monsterParameter.projectileDamage += (int)_buffParameter;
                    }

                    if (Type == "Effect")
                    {
                        Target.gameObject.GetComponent<MonsterController>().monsterParameter.monsterProjectile.gameObject.GetComponent<ProjectileController>().projectileParameter.effectOnEnd.transform.localScale *= 1 + _buffParameter;
                    }
                }
            }
        }
    }

    public int GetBulletIndex()
    {
        return _indexBullet;
    }
    
    public void SetBulletIndex(int index)
    {
        _indexBullet = index;
    }

    public float GetBuffParameter()
    {
        return _buffParameter;
    }
}
