using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletHudController : MonoBehaviour
{
    public GameObject player;
    private Transform _projectile;
    private Transform _bullet;
    
    private void OnEnable()
    {
        _projectile = transform.Find("Projectile");
        _bullet = transform.Find("Bullet");

        if (player)
        {
            _projectile.gameObject.GetComponent<Image>().sprite = player.gameObject.GetComponent<PlayerController>().GetCurrentProjectile().gameObject.GetComponent<SpriteRenderer>().sprite;
            _bullet.gameObject.GetComponent<TextMeshProUGUI>().text = player.gameObject.GetComponent<PlayerController>().GetBullet().ToString();

            player.gameObject.GetComponent<PlayerController>().OnProjectileChange += ProjectileUIControl;
            player.gameObject.GetComponent<PlayerController>().OnBulletChange += BulletUIControl;   
        }
    }

    private void OnDisable()
    {
        if (player)
        {
            player.gameObject.GetComponent<PlayerController>().OnProjectileChange -= ProjectileUIControl;
            player.gameObject.GetComponent<PlayerController>().OnBulletChange -= BulletUIControl;   
        }
    }

    private void ProjectileUIControl(Sprite projectileSprite)
    {
        _projectile.gameObject.GetComponent<Image>().sprite = projectileSprite;
    }
    
    private void BulletUIControl(int bullet)
    {
        _bullet.gameObject.GetComponent<TextMeshProUGUI>().text = bullet.ToString();
    }
}
