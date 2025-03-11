using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletHudController : MonoBehaviour
{
    private GameObject _player;
    private Transform _projectile;
    private Transform _bullet;
    
    private void OnEnable()
    {
        _player = GameObject.Find("Body");
        _projectile = transform.Find("Projectile");
        _bullet = transform.Find("Bullet");
        
        _projectile.gameObject.GetComponent<Image>().sprite = _player.gameObject.GetComponent<PlayerController>().projectile.gameObject.GetComponent<SpriteRenderer>().sprite;
        _bullet.gameObject.GetComponent<TextMeshProUGUI>().text = _player.gameObject.GetComponent<PlayerController>().GetBullet().ToString();

        _player.gameObject.GetComponent<PlayerController>().OnProjectileChange += ProjectileUIControl;
        _player.gameObject.GetComponent<PlayerController>().OnBulletChange += BulletUIControl;
    }

    private void OnDisable()
    {
        _player.gameObject.GetComponent<PlayerController>().OnProjectileChange -= ProjectileUIControl;
        _player.gameObject.GetComponent<PlayerController>().OnBulletChange -= BulletUIControl;
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
