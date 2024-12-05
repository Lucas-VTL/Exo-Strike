using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private int _playerSpeed;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        var horizontalMove = Input.GetAxisRaw("Horizontal");
        var verticalMove = Input.GetAxisRaw("Vertical");
        var move = new Vector3(horizontalMove, verticalMove, 0);

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
                _playerSpeed = 5;
            }
            else
            {
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isWalking", true);
                _playerSpeed = 3;
            }
            
            transform.position += move * (_playerSpeed * Time.deltaTime);
        }
    }
}