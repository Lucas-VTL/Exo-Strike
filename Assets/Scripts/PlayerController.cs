using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    
    private int _playerSpeed;
    private bool _isColliding = false;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
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

            if (!_isColliding)
            {
                var move = new Vector2(horizontalMove, verticalMove).normalized;
                _rigidbody.linearVelocity = move * _playerSpeed;     
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            _isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isColliding = false;
    }
}