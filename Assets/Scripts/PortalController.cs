using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public List<GameObject> monsters;
    
    private float _maxSpeed = 240f;
    private float _totalInitialTime = 3.5f;
    private float _elapseInitialTime = 0f;
    
    private float _destroyTime = 6;
    
    private float _totalDownScaleTime;
    private float _elapseDownScaleTime = 0f;
    
    private int _wave;
    
    void Start()
    {
        _totalDownScaleTime = _destroyTime - _totalInitialTime;
        InvokeRepeating("SpawnMonsters", _totalInitialTime, 1.5f);
        Destroy(gameObject, _destroyTime);
    }
    
    void Update()
    {
        _elapseInitialTime += Time.deltaTime;
        var spinSpeed = Mathf.Lerp(0f, _maxSpeed, _elapseInitialTime / _totalInitialTime);
        transform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
        
        if (_elapseInitialTime >= _totalInitialTime)
        {
            _elapseDownScaleTime += Time.deltaTime;
            var downScaleValue = Mathf.Lerp(1f, 0f, _elapseDownScaleTime / _totalDownScaleTime);
            transform.localScale =  new Vector3(downScaleValue, downScaleValue, downScaleValue);
        }
    }

    void SpawnMonsters()
    {
        var monsterIndex = Random.Range(0, monsters.Count);
        var monster = Instantiate(monsters[monsterIndex], transform.position, Quaternion.Euler(0f, 0f, 0f));
        monster.gameObject.GetComponent<MonsterController>().SetWaveScore(_wave - 1);
    }

    public void SetWave(int wave)
    {
        _wave = wave;
    }
}
