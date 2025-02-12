using System.Collections;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int speed;
    public int attackRange;
    public int health;
    public int damage;
    
    private GameObject _player;
    private Animator _animator;
    
    private Vector3 _oldPosition;
    
    private float _stuckingTimer = 2;
    private bool _alreadyStuck = false;
    
    private bool _isReturn = false;
    private readonly float _returnTimer = 1;
    private readonly float _minReturnAngle = 90;
    private readonly float _maxReturnAngle = 180;
    private Vector3 _returnDirection;
    private float _increaseReturnTime = 0f;
    private float _totalReturnTime;
    
    void Start()
    {
        _player = GameObject.Find("Body");
        _animator = GetComponent<Animator>();
        _oldPosition = transform.position;
    }
    
    void Update()
    {
        var angle = 0f;

        //Handle object rotation base on movement angle
        if (!_isReturn)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            angle =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Repeat(angle, 360f);
            transform.Translate(direction * (speed * Time.deltaTime));
        }
        else
        {
            angle =  Mathf.Atan2(_returnDirection.y, _returnDirection.x) * Mathf.Rad2Deg;
            angle = Mathf.Repeat(angle, 360f);
        }
        if (angle <= 90 || angle > 270)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (angle > 90 && angle <= 270)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        //Handle object behavior
        if (!_isReturn)
        {
            //Handle object animation in attack range
            var distance = Vector3.Distance(_player.transform.position, transform.position);
            if (distance <= attackRange)
            {
                _animator.SetBool("isAttackable", true);
                _stuckingTimer = 2;
            }
            else
            {
                _animator.SetBool("isAttackable", false);
            }
            
            //Handle object stucking case
            if (_stuckingTimer <= 0)
            {
                //Check stucking every _stuckingTimer seconds
                Vector3 currentPosition = transform.position;
                float transition = Vector3.Distance(currentPosition, _oldPosition);
                
                if (transition < speed)
                {
                    //Stucking case
                    var returnAngle = Random.Range(_minReturnAngle, _maxReturnAngle);
                    returnAngle = returnAngle * Mathf.Deg2Rad;
                    
                    Vector3 stuckDirection = _oldPosition - currentPosition;
                    _returnDirection = new Vector3(
                        stuckDirection.x * Mathf.Cos(returnAngle) - stuckDirection.y * Mathf.Sin(returnAngle), 
                        stuckDirection.x * Mathf.Sin(returnAngle) + stuckDirection.y * Mathf.Cos(returnAngle), 
                        0).normalized;
                    
                    _isReturn = true;

                    if (_alreadyStuck)
                    {
                        _increaseReturnTime += 0.5f;
                    }
                    
                    _totalReturnTime = _returnTimer + _increaseReturnTime;
                    _alreadyStuck = true;
                }
                else
                {
                    //Non stucking case
                    _oldPosition = currentPosition;
                    _stuckingTimer = 2;
                    _alreadyStuck = false;
                    _increaseReturnTime = 0f;
                }
            }
            else
            {
                _stuckingTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (_totalReturnTime <= 0)
            {
                //Stop returning period and reset data for new loop
                _isReturn = false;
                
                _stuckingTimer = 2;
                Vector3 currentPosition = transform.position;
                _oldPosition = currentPosition;
            }
            else
            {
                //Returning direction to escape stucking for _returnTimer seconds
                transform.Translate(_returnDirection * (speed * Time.deltaTime));
                _totalReturnTime -= Time.deltaTime;
            }
        }
    }
}
