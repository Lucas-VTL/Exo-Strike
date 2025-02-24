using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject moveScope;
    public Slider healthSlider;
    public Slider staminaSlider;

    private Animator _animator;

    private int _playerSpeed;
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

    private int _health = 10;
    private float _staminaMax = 3f;
    private float _currentStamina;
    private float _staminaCooldown = 1f;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
        _moveScopeCollider = moveScope.GetComponent<BoxCollider2D>();
        _xBoundary = _moveScopeCollider.size.x / 2;
        _yBoundary = _moveScopeCollider.size.y / 2;
        
        healthSlider.maxValue = _health;
        healthSlider.value = _health;
        
        staminaSlider.maxValue = _staminaMax;
        staminaSlider.value = _staminaMax;
        _currentStamina = _staminaMax;
    }

    private void Update()
    {
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
                Instantiate(projectile, gun.transform.position, Quaternion.Euler(0, 0, angleByDeg));
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            var damage = other.gameObject.GetComponent<MonsterController>().damage;
            
            _health -= damage;
            healthSlider.value = _health;

            if (_health <= 0)
            {
                Debug.Log("Game Over");
            }
        }
    }
}