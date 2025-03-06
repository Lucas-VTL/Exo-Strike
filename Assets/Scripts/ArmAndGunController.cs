using System;
using UnityEngine;

public class ArmAndGunController : MonoBehaviour
{
    private GameObject _player;
    
    void Start()
    {
        _player = GameObject.Find("Body");
    }
    

    //Handle when player being hit by monster's projectile or monster
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster Projectile") &&
            _player.gameObject.GetComponent<PlayerController>().GetInvulnerableTimer() <= 0)
        {
            var damage = other.gameObject.GetComponent<ProjectileController>().GetDamage();
            
            var health = _player.gameObject.GetComponent<PlayerController>().GetHealth();
            health -= damage;
            _player.gameObject.GetComponent<PlayerController>().SetHealth(health);
            
            var invulnerableTime = _player.gameObject.GetComponent<PlayerController>().GetInvulnerableTime();
            _player.gameObject.GetComponent<PlayerController>().SetInvulnerableTimer(invulnerableTime);

            if (_player.gameObject.GetComponent<PlayerController>().GetHealth() <= 0)
            {
                _player.gameObject.GetComponent<PlayerController>().EndGameEvent();
            }   
        }
        
        if (other.gameObject.CompareTag("Monster") &&
            _player.gameObject.GetComponent<PlayerController>().GetInvulnerableTimer() <= 0)
        {
            if (other.gameObject.GetComponent<MonsterController>().health > 0)
            {
                var damage = other.gameObject.GetComponent<MonsterController>().damage;
            
                var health = _player.gameObject.GetComponent<PlayerController>().GetHealth();
                health -= damage;
                _player.gameObject.GetComponent<PlayerController>().SetHealth(health);
                
                var invulnerableTime = _player.gameObject.GetComponent<PlayerController>().GetInvulnerableTime();
                _player.gameObject.GetComponent<PlayerController>().SetInvulnerableTimer(invulnerableTime);

                if (_player.gameObject.GetComponent<PlayerController>().GetHealth() <= 0)
                {
                    _player.gameObject.GetComponent<PlayerController>().EndGameEvent();
                }   
            }
        }
    }
}
