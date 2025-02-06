using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject moveScope;
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    
    private int _playerSpeed;
    private BoxCollider2D _moveScopeCollider;
    
    private float _xBoundary;
    private float _yBoundary;
    private float _xBoundaryOffset = 0.75f; 
    private float _yTopBoundaryOffset = 0.75f;
    private float _yBottomBoundaryOffset = 1.5f;

    public GameObject projectile;
    private Camera _mainCamera;
    private float _xBulletOffset = 0.4f;
    private float _yBulletOffset = 1.14f;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        _moveScopeCollider = moveScope.GetComponent<BoxCollider2D>();
        _xBoundary = _moveScopeCollider.size.x / 2;
        _yBoundary = _moveScopeCollider.size.y / 2;
    }
    
    private void Update()
    {
        var playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, -_xBoundary + _xBoundaryOffset, _xBoundary - _xBoundaryOffset);
        playerPos.y = Mathf.Clamp(playerPos.y, -_yBoundary + _yBottomBoundaryOffset, _yBoundary + _yTopBoundaryOffset);
        transform.position = playerPos;

        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            mousePos = _mainCamera.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
        
            Vector3 direction = (mousePos - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Repeat(angle, 360f);
            
            if (angle <= 60 || angle >= 300)
            {
                Vector3 bulletPos = transform.position + new Vector3(_xBulletOffset, _yBulletOffset, 0);
                Instantiate(projectile, bulletPos, Quaternion.Euler(0, 0, angle));
            }
            else if (angle >= 120 && angle <= 240)
            {
                Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angle));
            }
        }
    }
    
    private void FixedUpdate()
    {
        var horizontalMove = Input.GetAxisRaw("Horizontal");
        var verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove == 0 && verticalMove == 0)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
            _rigidbody.linearVelocity = Vector2.zero;
            _playerSpeed = 0;
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
            
            if (Input.GetKey(KeyCode.Space))
            {
                _animator.SetBool("isRunning", true);
                _animator.SetBool("isWalking", false);
                _playerSpeed = 7;
            }
            else
            {
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isWalking", true);
                _playerSpeed = 4;
            }
                
            var move = new Vector3(horizontalMove, verticalMove, 0).normalized;
            _rigidbody.linearVelocity = move * _playerSpeed;
        }
    }

    public int GetPlayerSpeed()
    {
        return _playerSpeed;
    }
}