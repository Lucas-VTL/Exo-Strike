﻿using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider staminaSlider;
    public GameObject gameOverPanel;

    private Animator _animator;

    private int _playerSpeed;
    private int _playerDamage = 1;
    private BoxCollider2D _moveScopeCollider;

    private float _xBoundary;
    private float _yBoundary;
    private float _xBoundaryOffset = 0.75f;
    private float _yTopBoundaryOffset = 0.75f;
    private float _yBottomBoundaryOffset = 1.5f;

    private Camera _mainCamera;

    public GameObject playerArm;
    private float _xArmOffset = 0.1f;
    private float _yArmOffset = 0.75f;

    public GameObject gun;
    private float _xGunOffset = 0.7f;
    private float _yGunOffset = 0.05f;

    public GameObject projectile;
    public GameObject projectileHeadLight;
    private float _xHeadGunOffset = 1.24f;
    private float _yHeadGunOffset = 0.05f;
    private GameObject _currentProjectile;

    private int _health = 10;
    private float _staminaMax = 3f;
    private float _currentStamina;
    private float _staminaCooldown = 1f;
    
    private float _invulnerableTime = 2f;
    private float _invulnerableTimer = 0f;
    private Transform _invulnerableShield;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;

        var moveScope = GameObject.Find("Move Boundary");
        _moveScopeCollider = moveScope.GetComponent<BoxCollider2D>();
        _xBoundary = _moveScopeCollider.size.x / 2;
        _yBoundary = _moveScopeCollider.size.y / 2;
        
        healthSlider.maxValue = _health;
        healthSlider.value = _health;
        
        staminaSlider.maxValue = _staminaMax;
        staminaSlider.value = _staminaMax;
        _currentStamina = _staminaMax;
        
        _invulnerableShield = transform.GetChild(0);
    }

    private void Update()
    {
        if (_invulnerableTimer > 0)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.gray;
            _invulnerableTimer -= Time.deltaTime;
            
            _invulnerableShield.gameObject.SetActive(true);
        }
        else
        {
            healthSlider.fillRect.GetComponent<Image>().color = new Color(197f / 255f, 17f / 255f, 0f, 1f);
            _invulnerableShield.gameObject.SetActive(false);
        }
        
        //Handle player not go out map boundaries
        var playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, -_xBoundary + _xBoundaryOffset, _xBoundary - _xBoundaryOffset);
        playerPos.y = Mathf.Clamp(playerPos.y, -_yBoundary + _yBottomBoundaryOffset, _yBoundary + _yTopBoundaryOffset);
        transform.position = playerPos;

        //Handle player movement and animation
        var horizontalMove = Input.GetAxisRaw("Horizontal");
        var verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove == 0 && verticalMove == 0)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
            _playerSpeed = 0;
            
            _staminaCooldown -= Time.deltaTime;
            
            if (_staminaCooldown <= 0)
            {
                _currentStamina = Mathf.Clamp(_currentStamina + Time.deltaTime, 0, _staminaMax);
                staminaSlider.value = _currentStamina;
            }
        }
        else
        {
            if (transform.localScale.x * horizontalMove > 0)
            {
                _animator.SetBool("isStepBack", false);
            }
            else
            {
                _animator.SetBool("isStepBack", true);
            }

            if (Input.GetKey(KeyCode.Space) && _currentStamina > 0)
            {
                _animator.SetBool("isRunning", true);
                _animator.SetBool("isWalking", false);
                _playerSpeed = 7;

                _currentStamina -= Time.deltaTime;
                _staminaCooldown = 1f;
                staminaSlider.value = _currentStamina;
            }
            else
            {
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isWalking", true);
                _playerSpeed = 4;

                _staminaCooldown -= Time.deltaTime;
                
                if (_staminaCooldown <= 0)
                {
                    _currentStamina = Mathf.Clamp(_currentStamina + Time.deltaTime, 0, _staminaMax);
                    staminaSlider.value = _currentStamina;
                }
            }

            var move = new Vector3(horizontalMove, verticalMove, 0).normalized;
            transform.Translate(move * (_playerSpeed * Time.deltaTime));
        }

        //Get mouse position and angleByDeg between mouse and main axis
        var mousePos = Input.mousePosition;
        mousePos = _mainCamera.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        Vector3 direction = (mousePos - playerArm.transform.position).normalized;
        var angleByRad = Mathf.Atan2(direction.y, direction.x);
        var angleByDeg = angleByRad * Mathf.Rad2Deg;
        angleByDeg = Mathf.Repeat(angleByDeg, 360f);

        var gunRadius = Mathf.Sqrt(_xGunOffset * _xGunOffset + _yGunOffset * _yGunOffset);
        var headGunRadius = Mathf.Sqrt(_xHeadGunOffset * _xHeadGunOffset + _yHeadGunOffset * _yHeadGunOffset);

        //Handle player rotation from mouse position
        if (angleByDeg <= 60 || angleByDeg >= 300)
        {
            transform.localScale = new Vector3(1, 1, 1);
            playerArm.transform.localScale = new Vector3(1, 1, 1);
            gun.transform.localScale = new Vector3(1, 1, 1);

            playerArm.transform.rotation = Quaternion.Euler(0, 0, angleByDeg);
            gun.transform.rotation = Quaternion.Euler(0, 0, angleByDeg);
        }
        else if (angleByDeg >= 120 && angleByDeg <= 240)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            playerArm.transform.localScale = new Vector3(-1, 1, 1);
            gun.transform.localScale = new Vector3(-1, 1, 1);

            playerArm.transform.rotation = Quaternion.Euler(0, 0, angleByDeg + 180);
            gun.transform.rotation = Quaternion.Euler(0, 0, angleByDeg + 180);
        }

        //Handle player arm position
        playerArm.transform.position = transform.position + new Vector3(_xArmOffset, _yArmOffset, 0);

        //Handle gun position
        if ((angleByDeg <= 60 || angleByDeg >= 300) || (angleByDeg >= 120 && angleByDeg <= 240))
        {
            gun.transform.position = playerArm.transform.position +
                                     new Vector3(gunRadius * Mathf.Cos(angleByRad), gunRadius * Mathf.Sin(angleByRad),
                                         0);
        }
        else
        {
            if (angleByDeg >= 60 && angleByDeg <= 120)
            {
                if (transform.localScale.x == 1)
                {
                    gun.transform.position = playerArm.transform.position +
                                             new Vector3(gunRadius * Mathf.Cos(60 * Mathf.Deg2Rad),
                                                 gunRadius * Mathf.Sin(60 * Mathf.Deg2Rad), 0);
                }
                else
                {
                    gun.transform.position = playerArm.transform.position +
                                             new Vector3(gunRadius * Mathf.Cos(120 * Mathf.Deg2Rad),
                                                 gunRadius * Mathf.Sin(120 * Mathf.Deg2Rad), 0);
                }
            }
            else if (angleByDeg >= 240 && angleByDeg <= 300)
            {
                if (transform.localScale.x == 1)
                {
                    gun.transform.position = playerArm.transform.position +
                                             new Vector3(gunRadius * Mathf.Cos(300 * Mathf.Deg2Rad),
                                                 gunRadius * Mathf.Sin(300 * Mathf.Deg2Rad), 0);
                }
                else
                {
                    gun.transform.position = playerArm.transform.position +
                                             new Vector3(gunRadius * Mathf.Cos(240 * Mathf.Deg2Rad),
                                                 gunRadius * Mathf.Sin(240 * Mathf.Deg2Rad), 0);
                }
            }
        }

        //Handle player shooting
        if ((angleByDeg <= 60 || angleByDeg >= 300) || (angleByDeg >= 120 && angleByDeg <= 240))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _currentProjectile = Instantiate(projectile, gun.transform.position, Quaternion.Euler(0, 0, angleByDeg));
                _currentProjectile.GetComponent<ProjectileController>().SetDamage(_playerDamage);
                
                GameObject headLight = null;
                if (angleByDeg <= 60 || angleByDeg >= 300)
                {
                    headLight = Instantiate(projectileHeadLight,
                        playerArm.transform.position + new Vector3(headGunRadius * Mathf.Cos(angleByRad),
                            headGunRadius * Mathf.Sin(angleByRad), 0), Quaternion.Euler(0, 0, 0));
                }
                else if (angleByDeg >= 120 && angleByDeg <= 240)
                {
                    headLight = Instantiate(projectileHeadLight,
                        playerArm.transform.position + new Vector3(headGunRadius * Mathf.Cos(angleByRad),
                            headGunRadius * Mathf.Sin(angleByRad), 0), Quaternion.Euler(0, 0, 180));
                }
                StartCoroutine(FollowHeadGun(headLight, headGunRadius));
            }
            
            healthSlider.value = _health;
        }
    }
    
    IEnumerator FollowHeadGun(GameObject origin, float radius)
    {
        var count = 10;
        
        while (count > 0)
        {
            var mousePos = Input.mousePosition;
            mousePos = _mainCamera.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
        
            Vector3 direction = (mousePos - playerArm.transform.position).normalized;
            var angleByRad = Mathf.Atan2(direction.y, direction.x);
            var angleByDeg = angleByRad * Mathf.Rad2Deg;
            angleByDeg = Mathf.Repeat(angleByDeg, 360f);
            
            if ((angleByDeg <= 60 || angleByDeg >= 300) || (angleByDeg >= 120 && angleByDeg <= 240))
            {
                origin.transform.position = playerArm.transform.position + new Vector3(radius * Mathf.Cos(angleByRad), radius * Mathf.Sin(angleByRad), 0);
            }
            else
            {
                if (angleByDeg >= 60 && angleByDeg <= 120)
                {
                    if (transform.localScale.x == 1)
                    {
                        origin.transform.position = playerArm.transform.position +
                                                    new Vector3(radius * Mathf.Cos(60 * Mathf.Deg2Rad),
                                                        radius * Mathf.Sin(60 * Mathf.Deg2Rad), 0);
                    }
                    else
                    {
                        origin.transform.position = playerArm.transform.position +
                                                    new Vector3(radius * Mathf.Cos(120 * Mathf.Deg2Rad),
                                                        radius * Mathf.Sin(120 * Mathf.Deg2Rad), 0);
                    }
                }
                else if (angleByDeg >= 240 && angleByDeg <= 300)
                {
                    if (transform.localScale.x == 1)
                    {
                        origin.transform.position = playerArm.transform.position +
                                                    new Vector3(radius * Mathf.Cos(300 * Mathf.Deg2Rad),
                                                        radius * Mathf.Sin(300 * Mathf.Deg2Rad), 0);
                    }
                    else
                    {
                        origin.transform.position = playerArm.transform.position +
                                                    new Vector3(radius * Mathf.Cos(240 * Mathf.Deg2Rad),
                                                        radius * Mathf.Sin(240 * Mathf.Deg2Rad), 0);
                    }
                }
            }

            count--;
            yield return null;
        }
        
        Destroy(origin);
        yield break;
    }

    //Handle when player being hit by monster
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != null && other.gameObject.GetComponent<PolygonCollider2D>() != null)
        {
            if (other.gameObject.CompareTag("Monster") && _invulnerableTimer <= 0)
            {
                if (other.gameObject.GetComponent<MonsterController>().health > 0 && other.gameObject.GetComponent<MonsterController>().hitDamage > 0)
                {
                    var damage = other.gameObject.GetComponent<MonsterController>().hitDamage;
        
                    _health -= damage;
                    _invulnerableTimer = _invulnerableTime;

                    if (_health <= 0)
                    {
                        EndGameEvent();
                    } 
                }
            }
        }
    }

    //Handle when player being hit by monster's projectile
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster Projectile") && _invulnerableTimer <= 0)
        {
            if (other.gameObject.GetComponent<ProjectileController>().GetDamage() > 0)
            {
                var damage = other.gameObject.GetComponent<ProjectileController>().GetDamage();
                
                _health -= damage;
                _invulnerableTimer = _invulnerableTime;

                if (_health <= 0)
                {
                    EndGameEvent();
                }   
            }
        }
    }

    public void SetHealth(int health)
    {
        _health = health;
    }

    public int GetHealth()
    {
        return _health;
    }

    public void EndGameEvent()
    {
        staminaSlider.value = 0;
        healthSlider.value = 0;
        gameObject.SetActive(false);
        playerArm.SetActive(false);
        gun.SetActive(false);
        GameObject.Find("Portal Spawning").GetComponent<PortalSpawning>().CancelInvoke();
        gameOverPanel.SetActive(true);
    }

    public void SetInvulnerableTimer(float invulnerableTime)
    {
        _invulnerableTimer = invulnerableTime;
    }

    public float GetInvulnerableTimer()
    {
        return _invulnerableTimer;
    }
    
    public float GetInvulnerableTime()
    {
        return _invulnerableTime;
    }
}