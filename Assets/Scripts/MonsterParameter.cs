using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterParamenter", menuName = "Monster Parameter")]
public class MonsterParameter : ScriptableObject
{
    public int monsterID;
    public float speed;
    public float attackRange;
    public int health;
    public int hitDamage;
    public int projectileDamage;
    public bool isMelee;
    public float attackCooldown;
    public float reviveTime;
    public float attackWaitingAngleRange;
    public GameObject monsterProjectile;
    public GameObject grave;
    public int score;
    public float xBoundaryOffset;
    public float yTopBoundaryOffset;
    public float yBottomBoundaryOffset;
    public float attackingTime;

    [NonSerialized]
    private int _monsterID;
    [NonSerialized]
    private float _speed;
    [NonSerialized]
    private float _attackRange;
    [NonSerialized]
    private int _health;
    [NonSerialized]
    private int _hitDamage;
    [NonSerialized]
    private int _projectileDamage;
    [NonSerialized]
    private bool _isMelee;
    [NonSerialized]
    private float _attackCooldown;
    [NonSerialized]
    private float _reviveTime;
    [NonSerialized]
    private float _attackWaitingAngleRange;
    [NonSerialized]
    private GameObject _monsterProjectile;
    [NonSerialized]
    private GameObject _grave;
    [NonSerialized]
    private int _score;
    [NonSerialized]
    private float _xBoundaryOffset;
    [NonSerialized]
    private float _yTopBoundaryOffset;
    [NonSerialized]
    private float _yBottomBoundaryOffset;
    [NonSerialized]
    private float _attackingTime;
    
    private void OnEnable()
    {
        _monsterID = monsterID;
        _speed = speed;
        _attackRange = attackRange;
        _health = health;
        _hitDamage = hitDamage;
        _projectileDamage = projectileDamage;
        _isMelee = isMelee;
        _attackCooldown = attackCooldown;
        _reviveTime = reviveTime;
        _attackWaitingAngleRange = attackWaitingAngleRange;
        _monsterProjectile = monsterProjectile;
        _grave = grave;
        _score = score;
        _xBoundaryOffset = xBoundaryOffset;
        _yTopBoundaryOffset = yTopBoundaryOffset;
        _yBottomBoundaryOffset = yBottomBoundaryOffset;
        _attackingTime = attackingTime;
    }

    private void OnDisable()
    {
        monsterID = _monsterID;
        speed = _speed;
        attackRange = _attackRange;
        health = _health;
        hitDamage = _hitDamage;
        projectileDamage = _projectileDamage;
        isMelee = _isMelee;
        attackCooldown = _attackCooldown;
        reviveTime = _reviveTime;
        attackWaitingAngleRange = _attackWaitingAngleRange;
        monsterProjectile = _monsterProjectile;
        grave = _grave;
        score = _score;
        xBoundaryOffset = _xBoundaryOffset;
        yTopBoundaryOffset = _yTopBoundaryOffset;
        yBottomBoundaryOffset = _yBottomBoundaryOffset;
        attackingTime = _attackingTime;
    }

    public void ResetData()
    {
        monsterID = _monsterID;
        speed = _speed;
        attackRange = _attackRange;
        health = _health;
        hitDamage = _hitDamage;
        projectileDamage = _projectileDamage;
        isMelee = _isMelee;
        attackCooldown = _attackCooldown;
        reviveTime = _reviveTime;
        attackWaitingAngleRange = _attackWaitingAngleRange;
        monsterProjectile = _monsterProjectile;
        grave = _grave;
        score = _score;
        xBoundaryOffset = _xBoundaryOffset;
        yTopBoundaryOffset = _yTopBoundaryOffset;
        yBottomBoundaryOffset = _yBottomBoundaryOffset;
        attackingTime = _attackingTime;
    }
}
