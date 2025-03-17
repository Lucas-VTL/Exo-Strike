using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortalSpawning : MonoBehaviour
{
    public GameObject player;
    public GameObject moveScope;
    public GameObject waterScope;
    public GameObject portal;
    public Slider waveSlider;
    public TextMeshProUGUI waveText;
    public GameObject cardHUD;
    
    private BoxCollider2D _moveScopeCollider;
    private PolygonCollider2D[] _waterScopeCollider;
    private float _xBoundary;
    private float _yBoundary;
    private float _xBoundaryOffset = 4f;
    private float _yTopBoundaryOffset = 1.5f;
    private float _yBottomBoundaryOffset = 4f;

    private bool _isAlreadySpawned = false;
    private int _wave;
    private float _waveTimer;
    private float _waveTimeMultiplier = 7f;
    private float _portalInitialTime = 4f;
    
    private Color _startSliderColor = new Color(0f / 255f, 255f / 255f, 72f / 255f);
    private Color _endSliderColor = new Color(255f / 255f, 20f / 255f, 0f / 255f);

    private Transform _cardPanel;
    
    void Start()
    {
        _moveScopeCollider = moveScope.GetComponent<BoxCollider2D>();
        _xBoundary = _moveScopeCollider.size.x / 2;
        _yBoundary = _moveScopeCollider.size.y / 2;
        
        _waterScopeCollider = waterScope.GetComponents<PolygonCollider2D>();
        _wave = 1;
        _waveTimer = _wave * _waveTimeMultiplier;
        waveSlider.maxValue = _waveTimer;
        waveSlider.value = _waveTimer;
        waveText.text = _wave.ToString();
        
        waveSlider.onValueChanged.AddListener(OnSliderChanged);
        _cardPanel = cardHUD.transform.GetChild(0);
        _cardPanel.gameObject.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    void Update()
    {
        if (player.activeSelf && Time.timeScale != 0)
        {
            if (_waveTimer > 0 && !_isAlreadySpawned)
            {
                for (int i = 0; i < _wave; i++)
                {
                    SpawnPortal();
                }
            
                _isAlreadySpawned = true;
            } 
            else if (_waveTimer > 0 && _isAlreadySpawned)
            {
                _waveTimer -= Time.deltaTime;
                waveSlider.value = _waveTimer;

                if (_waveTimer < _wave * _waveTimeMultiplier - _portalInitialTime)
                {
                    GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
                    if (monsters.Length == 0)
                    {   
                        Time.timeScale = 0f;
                        cardHUD.gameObject.SetActive(true);
                        _isAlreadySpawned = false;
                        _wave += 1;
                        _waveTimer = _wave * _waveTimeMultiplier;
                        waveSlider.maxValue = _waveTimer;
                        waveSlider.value = _waveTimer;
                        waveText.text = _wave.ToString();
                        return;
                    }
                }
            } 
            else
            {
                Time.timeScale = 0f;
                cardHUD.gameObject.SetActive(true);
                _isAlreadySpawned = false;
                _wave += 1;
                _waveTimer = _wave * _waveTimeMultiplier;
                waveSlider.maxValue = _waveTimer;
                waveSlider.value = _waveTimer;
                waveText.text = _wave.ToString();
            }   
        }

        if (Time.timeScale == 0)
        {
            _cardPanel.gameObject.GetComponent<Animator>().speed = 1f;
        }
    }
    
    void SpawnPortal()
    {
        bool isValid = false;
        Vector3 position = new Vector3();

        while (!isValid)
        {
            var randomX = Random.Range(-_xBoundary + _xBoundaryOffset, _xBoundary - _xBoundaryOffset);
            var randomY = Random.Range(-_yBoundary + _yBottomBoundaryOffset, _yBoundary + _yTopBoundaryOffset);
            
            position.x = randomX;
            position.y = randomY;
            position.z = 0;

            if (!IsInWaterZone(position))
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
        }
        
        var newPortal = Instantiate(portal, position, Quaternion.Euler(0, 0, 0));
        newPortal.gameObject.GetComponent<PortalController>().SetWave(_wave);
    }

    bool IsInWaterZone(Vector3 point)
    {
        foreach (var zone in _waterScopeCollider)
        {
            if (zone.OverlapPoint(point))
            {
                return true;
            }
        }
        
        return false;
    }
    
    private void OnSliderChanged(float value)
    {
        var t = value / waveSlider.maxValue;
        var newRed =  Mathf.Lerp(_endSliderColor.r, _startSliderColor.r, t);
        var newGreen =  Mathf.Lerp(_endSliderColor.g,_startSliderColor.g, t);
        var newBlue =  Mathf.Lerp(_endSliderColor.b, _startSliderColor.b, t);
        waveSlider.fillRect.GetComponent<Image>().color = new Color(newRed, newGreen, newBlue, 1);
    }
    
    void OnDestroy()
    {
        waveSlider.onValueChanged.RemoveAllListeners();
    }
}
