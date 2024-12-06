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
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveScopeCollider = moveScope.GetComponent<BoxCollider2D>();
        _xBoundary = _moveScopeCollider.size.x / 2;
        _yBoundary = _moveScopeCollider.size.y / 2;
    }
    
    private void FixedUpdate()
    {
        var horizontalMove = Input.GetAxisRaw("Horizontal");
        var verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove == 0 && verticalMove == 0)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
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

    private void Update()
    {
        var playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, -_xBoundary + _xBoundaryOffset, _xBoundary - _xBoundaryOffset);
        playerPos.y = Mathf.Clamp(playerPos.y, -_yBoundary + _yBottomBoundaryOffset, _yBoundary + _yTopBoundaryOffset);
        transform.position = playerPos;
    }
}