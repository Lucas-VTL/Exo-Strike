using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider staminaSlider;
    public GameObject gameOverPanel;
    
    private Animator _animator;

    private int _playerWalkSpeed = 4;
    private int _playerRunSpeed = 7;
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

    public GameObject[] projectileList;
    public GameObject projectileHeadLight;
    private float _xHeadGunOffset = 1.24f;
    private float _yHeadGunOffset = 0.05f;
    private int _currentProjectileIndex;
    private int[] _projectileDamage;

    private int _health = 10;
    private float _staminaMax = 5f;
    private float _currentStamina;
    private float _staminaCooldown = 1f;
    
    private float _invulnerableTime = 2f;
    private float _invulnerableTimer = 0f;
    private Transform _invulnerableShield;

    private bool _canFire;
    private int[] _bulletStorage = {30, 10, 1};
    private int[] _bullet = {30, 10, 1};
    private float[] _reloadTime = {2f, 2.5f, 1f};
    private float _reloadTimer;
    private bool _isReload;
    
    public event Action<int> OnBulletChange;
    public event Action<bool> OnShootAngleChange;
    public event Action<Sprite> OnProjectileChange;
    public event Action<bool> OnReload;
    public event Action<int> OnDead;

    private int _score;
    private bool _isScrolling;
    private float _scrollingTimer;

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
        _isReload = false;
        _score = 0;
        
        _currentProjectileIndex = 0;
        _projectileDamage = new int[projectileList.Length];
        for (int i = 0; i < projectileList.Length; i++)
        {
            _projectileDamage[i] = 1;
        }
        
        _isScrolling = false;
        
        OnBulletChange?.Invoke(_bullet[_currentProjectileIndex]);
    }

    private void Update()
    {
        if (Time.timeScale != 0)
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
            
                    _currentStamina -= Time.deltaTime;
                    _staminaCooldown = 1f;
                    staminaSlider.value = _currentStamina;
                    
                    var move = new Vector3(horizontalMove, verticalMove, 0).normalized;
                    transform.Translate(move * (_playerRunSpeed * Time.deltaTime));
                }
                else
                {
                    _animator.SetBool("isRunning", false);
                    _animator.SetBool("isWalking", true);
            
                    _staminaCooldown -= Time.deltaTime;
                    
                    if (_staminaCooldown <= 0)
                    {
                        _currentStamina = Mathf.Clamp(_currentStamina + Time.deltaTime, 0, _staminaMax);
                        staminaSlider.value = _currentStamina;
                    }
                    
                    var move = new Vector3(horizontalMove, verticalMove, 0).normalized;
                    transform.Translate(move * (_playerWalkSpeed * Time.deltaTime));
                }
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
            
            //Handle player's projectile type
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
            
            if (mouseScroll != 0 && !_isScrolling)
            {
                if (mouseScroll > 0)
                {
                    _currentProjectileIndex++;
                }
                else if (mouseScroll < 0)
                {
                    _currentProjectileIndex--;
                }
                
                _currentProjectileIndex = (int)Mathf.Repeat(_currentProjectileIndex, projectileList.Length);
                
                Reload(false);
                _isReload = false;
                _reloadTimer = 0;
                
                OnBulletChange?.Invoke(_bullet[_currentProjectileIndex]);
                
                ProjectileChange();
                _isScrolling = true;
                _scrollingTimer = 0.25f;
            }
            
            if (mouseScroll == 0)
            {
                _scrollingTimer -= Time.deltaTime;
            }
            
            if (_scrollingTimer <= 0)
            {
                _isScrolling = false;
            }
            
            //Handle player shooting
            if (_reloadTimer > 0)
            {
                _reloadTimer -= Time.deltaTime;
                return;
            }
            else
            {
                if (_isReload)
                {
                    Reload(false);
                    OnBulletChange?.Invoke(_bulletStorage[_currentProjectileIndex]);
                    _isReload = false;
                    _bullet[_currentProjectileIndex] = _bulletStorage[_currentProjectileIndex];
                }
                
                if ((angleByDeg <= 60 || angleByDeg >= 300) || (angleByDeg >= 120 && angleByDeg <= 240))
                {
                    OnShootAngleChange(true);
                    _canFire = true;
                
                    if (Input.GetMouseButtonDown(0))
                    {
                        var projectile = Instantiate(projectileList[_currentProjectileIndex], gun.transform.position, Quaternion.Euler(0, 0, angleByDeg));
                        projectile.gameObject.GetComponent<ProjectileController>().SetDamage(_projectileDamage[_currentProjectileIndex]);
                        
                        GameObject headLight = null;
                        if (angleByDeg <= 60 || angleByDeg >= 300)
                        {
                            headLight = Instantiate(projectileHeadLight,
                                playerArm.transform.position + new Vector3(headGunRadius * Mathf.Cos(angleByRad),
                                    headGunRadius * Mathf.Sin(angleByRad), 0), Quaternion.Euler(0, 0, 0));
                            BulletChange();
                        }
                        else if (angleByDeg >= 120 && angleByDeg <= 240)
                        {
                            headLight = Instantiate(projectileHeadLight,
                                playerArm.transform.position + new Vector3(headGunRadius * Mathf.Cos(angleByRad),
                                    headGunRadius * Mathf.Sin(angleByRad), 0), Quaternion.Euler(0, 0, 180));
                            BulletChange();
                        }
                        StartCoroutine(FollowHeadGun(headLight, headGunRadius));
                    }
                } 
                else
                {
                    OnShootAngleChange(false);
                    _canFire = false;
                }   
            }
            
            //Handle player manual reloading
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_bullet[_currentProjectileIndex] < _bulletStorage[_currentProjectileIndex])
                {
                    Reload(true);
                    _reloadTimer = _reloadTime[_currentProjectileIndex];
                    _isReload = true;   
                }
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
    
    //Handle when player stand in a trap
    private void OnTriggerStay2D(Collider2D other)
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
        OnDead?.Invoke(_score);
        staminaSlider.value = 0;
        healthSlider.value = 0;
        gameObject.SetActive(false);
        playerArm.SetActive(false);
        gun.SetActive(false);
        projectileHeadLight.SetActive(false);
        Reload(false);
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

    public void SetBulletStorage(int bullet)
    {
        _bulletStorage[_currentProjectileIndex] = bullet;
    }

    public int GetBullet()
    {
        return _bullet[_currentProjectileIndex];
    }

    public float GetReloadTime()
    {
        return _reloadTime[_currentProjectileIndex];
    }

    void BulletChange()
    {
        if (_bullet[_currentProjectileIndex] > 0)
        {
            _bullet[_currentProjectileIndex]--;

            if (_bullet[_currentProjectileIndex] == 0)
            {
                Reload(true);
                _reloadTimer = _reloadTime[_currentProjectileIndex];
                _isReload = true;
            }
        }
        else
        {
            _bullet[_currentProjectileIndex] = _bulletStorage[_currentProjectileIndex];
        }
        
        OnBulletChange?.Invoke(_bullet[_currentProjectileIndex]);
    }

    void ProjectileChange()
    {
        if (_bullet[_currentProjectileIndex] == 0)
        {
            Reload(true);
            _reloadTimer = _reloadTime[_currentProjectileIndex];
            _isReload = true;
        }
        
        OnBulletChange?.Invoke(_bullet[_currentProjectileIndex]);
        OnProjectileChange?.Invoke(projectileList[_currentProjectileIndex].gameObject.GetComponent<SpriteRenderer>().sprite);
    }

    void ShootAngleChange(bool canFire)
    {
        if (_canFire != canFire)
        {
            OnShootAngleChange?.Invoke(_canFire);
        }
    }

    void Reload(bool isReload)
    {
        OnReload?.Invoke(isReload);
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    public GameObject GetCurrentProjectile()
    {
        return projectileList[_currentProjectileIndex];
    }
}