using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MonsterController : MonoBehaviour
{
    public int speed;
    public float attackRange;
    public int health;
    public int hitDamage;
    public int projectileDamage;
    public bool isMelee;
    public float attackCooldown;
    public float attackWaitingAngleRange;
    public GameObject monsterProjectile;
    
    private GameObject _player;
    private Animator _animator;
    private Slider _healthSlider;
    private GameObject _currentMonsterProjectile;
    
    private Vector3 _oldPosition;
    
    private float _stuckingTimer = 2;
    private bool _alreadyStuck = false;
    
    private bool _isReturn = false;
    private readonly float _returnTimer = 1;
    private readonly float _minReturnAngle = 90;
    private readonly float _maxReturnAngle = 270;
    private Vector3 _returnDirection;
    private float _increaseReturnTime = 0f;
    private float _totalReturnTime;
    private bool _isDead = false;
    
    public float attackingTime;
    private float _attackingTimer = 0f;
    private bool _isFinishAttacking = true;
    private float _attackCooldownTimer = 0f;
    private float _attackWaitingAngle;
    private bool _isAttackWaitingAngleExist = false;
    
    private Color _startHealthSliderColor = new Color(0f / 255f, 255f / 255f, 72f / 255f);
    private Color _endHealthSliderColor = new Color(255f / 255f, 20f / 255f, 0f / 255f);
    
    void Start()
    {   
        _player = GameObject.Find("Body");

        if (_player == null)    
        {
            Destroy(gameObject);
        }
        
        _animator = GetComponent<Animator>();
        _healthSlider = GetComponentInChildren<Slider>();
        _healthSlider.onValueChanged.AddListener(OnHealthSliderChanged);
        
        _oldPosition = transform.position;
        _healthSlider.maxValue = health;
        _healthSlider.value = health;
    }
    
    void Update()
    {
        if (!_player.gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
        else
        {
            if (!_isDead)
            {
                //Get direction between monster and player
                var angle = 0f;
                Vector3 direction = (_player.transform.position - transform.position).normalized;
                angle =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
                if (!AttackHandling(angle))
                {
                    return;
                }
                MovementHandling(direction, angle);
                
                //Handle object behavior
                if (!_isReturn)
                {
                    AnimationHandling();
                    StuckingHandling();
                }
                else
                {
                    ReturnHandling();
                }   
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            var damage = other.gameObject.GetComponent<ProjectileController>().GetDamage();
            
            health -= damage;
            _healthSlider.value = health;

            if (health <= 0)
            {
                _isDead = true;
                _animator.SetBool("isDead", true);
                Destroy(gameObject, 1f);
            }
        }
    }

    //Handle object rotation base on movement angle
    void MovementHandling(Vector3 direction, float angle)
    {
        if (!_isReturn)
        {
            transform.Translate(direction * (speed * Time.deltaTime));   
        }
        else
        {
            angle = Mathf.Atan2(_returnDirection.y, _returnDirection.x) * Mathf.Rad2Deg;
        }
                
        angle = Mathf.Repeat(angle, 360f);
                
        if (angle <= 90 || angle > 270)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (angle > 90 && angle <= 270)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    //Handle object stucking case
    void StuckingHandling()
    {
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

    //Handle object returning case
    void ReturnHandling()
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

    //Handle object attacking event
    bool AttackHandling(float angle)
    {
        //Handle when object launch the attack
        if (_attackingTimer > 0)
        {
            _attackingTimer -= Time.deltaTime;
            return false;
        }
        else
        {
            if (!isMelee && !_isFinishAttacking)
            {
                _currentMonsterProjectile = Instantiate(monsterProjectile, transform.position, Quaternion.Euler(0, 0, angle));
                _currentMonsterProjectile.GetComponent<ProjectileController>().SetDamage(projectileDamage);
                _attackCooldownTimer = attackCooldown;
            }
            _isFinishAttacking = true;
        }

        //Handle when object on attack cooldown
        if (_attackCooldownTimer > 0)
        {
            if (!_isAttackWaitingAngleExist)
            {
                _attackWaitingAngle = Random.Range(0, 2) == 0 ? attackWaitingAngleRange : -attackWaitingAngleRange;
                _attackWaitingAngle *= Mathf.Deg2Rad;
                _isAttackWaitingAngleExist = true;
            }
            
            _attackCooldownTimer -= Time.deltaTime;
            
            Vector3 targetDirection = _player.transform.position - transform.position;
            
            var moveDirection = new Vector3(
                targetDirection.x * Mathf.Cos(_attackWaitingAngle) - targetDirection.y * Mathf.Sin(_attackWaitingAngle), 
                targetDirection.x * Mathf.Sin(_attackWaitingAngle) + targetDirection.y * Mathf.Cos(_attackWaitingAngle), 
                0).normalized;
                    
            _animator.SetBool("isAttackable", false);
            transform.Translate(moveDirection * (speed * Time.deltaTime));
            
            var newAngle = 0f;
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            newAngle =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    
            newAngle = Mathf.Repeat(newAngle, 360f);
                
            if (newAngle <= 90 || newAngle > 270)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (newAngle > 90 && newAngle <= 270)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            
            return false;
        }
        else
        {
            _isAttackWaitingAngleExist = false;
        }
        
        return true;
    }

    //Handle object animation base on attack range
    void AnimationHandling()
    {
        var distance = Vector3.Distance(_player.transform.position, transform.position);
        if ((distance <= attackRange) && (_attackCooldownTimer <= 0) && (_attackingTimer <= 0))
        {
            _animator.SetBool("isAttackable", true);
            _stuckingTimer = 2;
            _attackingTimer = attackingTime;
            _isFinishAttacking = false;
        } 
        else
        {
            _animator.SetBool("isAttackable", false);
        }
    }

    void OnHealthSliderChanged(float value)
    {
        var t = value / _healthSlider.maxValue;
        var newRed =  Mathf.Lerp(_endHealthSliderColor.r, _startHealthSliderColor.r, t);
        var newGreen =  Mathf.Lerp(_endHealthSliderColor.g,_startHealthSliderColor.g, t);
        var newBlue =  Mathf.Lerp(_endHealthSliderColor.b, _startHealthSliderColor.b, t);
        _healthSlider.fillRect.GetComponent<Image>().color = new Color(newRed, newGreen, newBlue, 1);
    }

    void OnDestroy()
    {
        _healthSlider.onValueChanged.RemoveAllListeners();
    }
}
