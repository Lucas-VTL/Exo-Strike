using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletHudController : MonoBehaviour
{
    public GameObject player;
    private Transform _projectile;
    private Transform _bullet;
    private Transform _damage;
    
    private void OnEnable()
    {
        _projectile = transform.Find("Projectile");
        _bullet = transform.Find("Bullet");
        _damage = transform.Find("Damage");

        if (player)
        {
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
        
        var damage = player.gameObject.GetComponent<PlayerController>().GetCurrentProjectileDamage();
        
        for (int i = 0; i < damage; i++)
        {
            _damage.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = damage; i < 10; i++)
        {
            _damage.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
    private void BulletUIControl(int bullet)
    {
        _bullet.gameObject.GetComponent<TextMeshProUGUI>().text = bullet.ToString();
    }
}
